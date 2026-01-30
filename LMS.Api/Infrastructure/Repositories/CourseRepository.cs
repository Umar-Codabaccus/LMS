using LMS.Api.Application.DTOs;
using LMS.Api.Domain.Entities;
using LMS.Api.Domain.Enums;
using LMS.Api.Infrastructure.Context;
using LMS.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Api.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Course> _db;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
            _db = context.Set<Course>();
        }

        public Course Add(Course course)
        {
            try
            {
                _db.Add(course);
                _context.SaveChanges();
                return course;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Delete(Course course)
        {
            try
            {
                _db.Remove(course);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Course Get(Guid courseId)
        {
            var course = _db.Find(courseId);
            return course;
        }

        public List<Course> GetAll()
        {
            var courses = _db.ToList();
            return courses;
        }

        public List<Course> GetCoursesBySearch(string searchText)
        {
            var courses = _db.Where(course => course.Title.ToLower().Contains(searchText.ToLower()))
                            .ToList();
            return courses;
        }

        public bool Update(Course course)
        {
            try
            {
                _db.Update(course);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<PublishedCourse> GetCoursesForUser(Guid userId)
        {
            var courses = _db
                .Where(course => 
                    course.Status == CourseStatus.Published.ToString() && 
                    !course.EnrolledUsers.Any(enrolledUser => enrolledUser.UserId == userId))
                .ToList();


            return courses
                .Select(course => new PublishedCourse()
                {
                    CourseId = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Status = course.Status,
                    ImageUrl = course.ThumbnailUrl,
                    Level = course.Level
                })
                .ToList();
        }
    }
}