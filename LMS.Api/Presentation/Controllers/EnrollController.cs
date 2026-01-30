using LMS.Api.Application.DTOs.Enrollment;
using LMS.Api.Application.Services.Interfaces;
using LMS.Api.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost("enroll")]
        public ActionResult<EnrollResponse> Enroll([FromBody] EnrollRequest request)
        {
            var responseResult = _enrollmentService.Enroll(request);

            if (responseResult.Error != Error.None)
            {
                return BadRequest(responseResult.Error.Message);
            }

            return Ok(responseResult.Value);
        }

        [HttpGet("enrolled-courses/{userId}")]
        public ActionResult GetEnrolledCourses(Guid userId)
        {
            var response = _enrollmentService.GetEnrolledCourses(userId);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
