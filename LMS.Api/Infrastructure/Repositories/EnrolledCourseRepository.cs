using LMS.Api.Application.DTOs.Enrollment;
using LMS.Api.Application.DTOs.Module;
using LMS.Api.Domain.Entities;
using LMS.Api.Infrastructure.Context;
using LMS.Api.Infrastructure.Interfaces;
using LMS.Api.Shared;
using Microsoft.EntityFrameworkCore;

namespace LMS.Api.Infrastructure.Repositories
{
    public class EnrolledCourseRepository : IEnrolledCourseRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<EnrolledCourse> _db;

        private string className = nameof(EnrolledCourseRepository);

        public EnrolledCourseRepository(AppDbContext context)
        {
            _context = context;
            _db = context.Set<EnrolledCourse>();
        }

        public string CheckExistence(Guid courseId, Guid userId)
        {
            try
            {
                var course = _db.FirstOrDefault(course => 
                    course.CourseId == courseId && course.UserId == userId);

                if (course == null)
                {
                    // course does not exist
                    return Messages.EnrolledMessages.DoNotExist();
                }

                return Messages.EnrolledMessages.Exist();
            }
            catch (Exception e)
            {
                string location = $"{className} {nameof(CheckExistence)}";
                return Messages.Failures.DatabaseFailure(e, location);
            }
        }

        public (EnrolledCourse?, string) Add(EnrolledCourse enrolledCourse)
        {
            try
            {
                _db.Add(enrolledCourse);
                _context.SaveChanges();
                string message = "Enrollment Successful";

                return (enrolledCourse, message);
            }
            catch (Exception e)
            {
                string location = $"Class: {className} -- {nameof(Add)}";
                string message = Messages.Failures.DatabaseFailure(e, location);

                return (null, message);
            }
        }

        public (List<EnrolledModule>?, string) EnrollModules(Queue<EnrolledModule> modules)
        {
            List<EnrolledModule> enrolledModules = [];
            try
            {
                while (modules.Count > 0)
                {
                    var module = modules.Dequeue();
                    _context.EnrolledModules.Add(module);
                    enrolledModules.Add(module);
                }

                _context.SaveChanges();

                return (enrolledModules, string.Empty);
            } 
            catch (Exception e)
            {
                return (null, Messages.Failures.DatabaseFailure(e, $"Class: {className} -- {nameof(EnrollModules)}"));
            }
        }

        // Get courses enrolled by a user
        public List<EnrollCourseDto> GetEnrolledCourses(Guid userId)
        {
            var courses = (
                from ec in _context.EnrolledCourses
                join u in _context.Users on ec.UserId equals u.Id
                join c in _context.Courses on ec.CourseId equals c.Id
                join em in _context.EnrolledModules on ec.Id equals em.EnrolledCourseId
                join m in _context.Modules on em.ModuleId equals m.Id
                where u.Id == userId
                group new { m } by new
                {
                    c.Id,
                    c.Title,
                    c.Description,
                    c.Level,
                    c.ThumbnailUrl,
                    ec.Status
                }
                into g
                select new EnrollCourseDto
                {
                    CourseId = g.Key.Id,
                    Title = g.Key.Title,
                    Description = g.Key.Description,
                    Level = g.Key.Level,
                    ThumbnailUrl = g.Key.ThumbnailUrl,
                    Status = g.Key.Status,
                    EnrollModules = g.Select(x => new EnrollModuleDto
                    {
                        ModuleId = x.m.Id,
                        Title = x.m.Title,
                        Description = x.m.Description,
                        VideoUrl = x.m.VideoUrl,
                        Order = x.m.ModuleOrder
                    }).ToList()
                }
            ).ToList();

            return courses;
        }
    }
}
