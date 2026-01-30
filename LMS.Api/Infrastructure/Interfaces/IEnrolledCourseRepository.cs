using LMS.Api.Application.DTOs.Enrollment;
using LMS.Api.Domain.Entities;

namespace LMS.Api.Infrastructure.Interfaces
{
    public interface IEnrolledCourseRepository
    {
        public string CheckExistence(Guid courseId, Guid userId);
        public (EnrolledCourse, string) Add(EnrolledCourse enrolledCourse);
        public (List<EnrolledModule>?, string) EnrollModules(Queue<EnrolledModule> modules);
        public List<EnrollCourseDto> GetEnrolledCourses(Guid userId);
    }
}
