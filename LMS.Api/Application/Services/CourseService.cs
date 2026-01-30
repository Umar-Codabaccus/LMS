using LMS.Api.Application.DTOs;
using LMS.Api.Application.Errors;
using LMS.Api.Application.Services.Interfaces;
using LMS.Api.Domain.Entities;
using LMS.Api.Domain.Enums;
using LMS.Api.Infrastructure.Interfaces;

namespace LMS.Api.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IModuleRepository _moduleRepository;

        public CourseService(ICourseRepository courseRepository, IModuleRepository moduleRepository)
        {
            _courseRepository = courseRepository;
            _moduleRepository = moduleRepository;
        }

        public CourseResponse CreateCourse(Guid userId, CourseDto dto)
        {
            // create a new course object
            var course = new Course()
            {
                Title = dto.Title,
                Description = dto.Description,
                Level = dto.Level,
                ThumbnailUrl = dto.ThumbnailUrl,
                Status = CourseStatus.Draft.ToString(),
                InstructorId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var addedCourse = _courseRepository.Add(course);
            
            if (addedCourse is null)
            {
                return new CourseResponse()
                {
                    IsCourseCreated = false,
                    Message = DomainErrors.CourseErrors.CourseCreationFailed()
                };
            }

            return new CourseResponse()
            {
                IsCourseCreated = true,
                Message = "Course Creation Successful"
            };
        }

        public CourseResponse DeleteCourse(Guid courseId)
        {
            var course = _courseRepository.Get(courseId);

            if (course is null)
            {
                return new CourseResponse()
                {
                    IsCourseDeleted = false,
                    Message = DomainErrors.CourseErrors.CourseDeletionFailed()
                };
            }

            var isCourseDeleted = _courseRepository.Delete(course);

            if (!isCourseDeleted)
            {
                return new CourseResponse()
                {
                    IsCourseDeleted = false,
                    Message = DomainErrors.CourseErrors.CourseDeletionFailed()
                };
            }

            return new CourseResponse()
            {
                IsCourseDeleted = true,
                Message = "Course Deleted Successfully"
            };
        }

        public CourseList GetCourses()
        {
            var courses = _courseRepository.GetAll();

            if (courses?.Count == 0)
            {
                return new CourseList()
                {
                    Message = DomainErrors.CourseErrors.NoCourses(),
                    Error = true
                };
            }

            List<GetCourseDto> courseList = courses
                .Select(course => new GetCourseDto()
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Level = course.Level,
                    ThumbnailUrl = course.ThumbnailUrl,
                    UserId = course.InstructorId,
                    Status = course.Status
                })
                .ToList();

            return new CourseList()
            {
                Courses = courseList,
                Error = false
            };
        }

        public CourseList GetSearchedCourses(string searchText)
        {
            var courses = _courseRepository.GetCoursesBySearch(searchText);

            if (courses?.Count == 0)
            {
                return new CourseList()
                {
                    Message = DomainErrors.CourseErrors.NoCourses(),
                    Error = true
                };
            }

            List<GetCourseDto> courseList = courses
                .Select(course => new GetCourseDto()
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Level = course.Level,
                    ThumbnailUrl = course.ThumbnailUrl,
                    UserId = course.InstructorId,
                    Status = course.Status
                })
                .ToList();

            return new CourseList()
            {
                Courses = courseList,
                Error = false
            };
        }

        public CourseResponse PublishCourse(Guid courseId)
        {
            var course = _courseRepository.Get(courseId);

            // The course is unavailable
            if (course is null)
            {
                return new CourseResponse()
                {
                    IsCoursePublished = false,
                    Message = DomainErrors.CourseErrors.CoursePublicationFailed()
                };
            }

            var modules = _moduleRepository.GetAll(course.Id);

            // The course does not hava any modules
            if (modules?.Count == 0)
            {
                return new CourseResponse()
                {
                    IsCoursePublished = false,
                    Message = DomainErrors.CourseErrors.CourseCannotBePublishedDueToNoModulesPresent()
                };
            }

            // The course is already published
            if (course.Status == CourseStatus.Published.ToString())
            {
                return new CourseResponse()
                {
                    IsCoursePublished = false,
                    Message = DomainErrors.CourseErrors.CourseAlreadyPublished()
                };
            }

            course.Status = CourseStatus.Published.ToString();
            course.PublishedDate = DateTime.Now;

            var isCoursePublished = _courseRepository.Update(course);

            if (!isCoursePublished)
            {
                return new CourseResponse()
                {
                    IsCoursePublished = false,
                    Message = DomainErrors.CourseErrors.CoursePublicationFailed()
                };
            }

            return new CourseResponse()
            {
                IsCoursePublished = true,
                Message = "Course Published Successfully"
            };
        }

        public CourseResponse UpdateCourse(Guid courseId, CourseDto dto)
        {
            var course = _courseRepository.Get(courseId);

            if (course is null)
            {
                return new CourseResponse()
                {
                    IsCourseUpdated = false,
                    Message = DomainErrors.CourseErrors.UpdatingCourseFailed()
                };
            }

            course.Title = dto.Title;
            course.Description = dto.Description;
            course.Level = dto.Level;
            course.ThumbnailUrl = dto.ThumbnailUrl;
            course.UpdatedAt = DateTime.Now;

            bool isCourseUpdated = _courseRepository.Update(course);

            if (!isCourseUpdated)
            {
                return new CourseResponse()
                {
                    IsCourseUpdated = false,
                    Message = DomainErrors.CourseErrors.UpdatingCourseFailed()
                };
            }

            return new CourseResponse()
            {
                IsCourseUpdated = true,
                Message = "Course Updated Successfully"
            };
        }

        // Function to get courses that has been published and not enrolled by the user
        public (List<PublishedCourse>, string) GetPublishedCoursesForUser(Guid userId)
        {
            var courses = _courseRepository.GetCoursesForUser(userId);

            if (courses is null)
            {
                return new(new List<PublishedCourse>(), DomainErrors.CourseErrors.NoCourses());
            }

            return new (courses.ToList(), "Published Courses Fetched Successfully");
        }
    }
}
