using LMS.Api.Application.DTOs;

namespace LMS.Api.Application.Services.Interfaces
{
    public interface ICourseService
    {
        public CourseResponse CreateCourse(Guid userId, CourseDto dto);
        public CourseResponse UpdateCourse(Guid courseId, CourseDto dto);
        public CourseResponse DeleteCourse(Guid courseId);
        public CourseList GetCourses();
        public CourseList GetSearchedCourses(string searchText);
        public CourseResponse PublishCourse(Guid courseId);
        public (List<PublishedCourse>, string) GetPublishedCoursesForUser(Guid userId);
    }
}