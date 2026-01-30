using LMS.Api.Application.DTOs.Enrollment;
using LMS.Api.Shared;

namespace LMS.Api.Application.Services.Interfaces
{
    public interface IEnrollmentService
    {
        public Result<EnrollResponse> Enroll(EnrollRequest request);
        public List<EnrollCourseDto> GetEnrolledCourses(Guid userId);
    }
}
