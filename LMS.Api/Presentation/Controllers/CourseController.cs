using LMS.Api.Application.DTOs;
using LMS.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("{userId}/create")]
        public ActionResult CreateCourse(Guid userId, [FromBody] CourseDto dto)
        {
            var result = _courseService.CreateCourse(userId, dto);

            if (!result.IsCourseCreated)
            {
                return BadRequest(result.Message);
            }

            return Created();
        }

        // Update Course
        [HttpPut("update/{id}")]
        public ActionResult UpdateCourse(Guid id, [FromBody] CourseDto dto)
        {
            var result = _courseService.UpdateCourse(id, dto);

            if (!result.IsCourseUpdated)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // Delete Course
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteCourse(Guid id)
        {
            var result = _courseService.DeleteCourse(id);

            if (!result.IsCourseDeleted)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // Get All Courses
        [HttpGet("courses")]
        public ActionResult<CourseList> GetCourses()
        {
            var result = _courseService.GetCourses();

            if (result.Error)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Courses);
        }

        // Search Course
        [HttpGet("courses/search")]
        public ActionResult SearchCourses([FromQuery] string? searchText)
        {
            Console.WriteLine($"Search Text: {searchText}");

            if (string.IsNullOrWhiteSpace(searchText))
            {
                return Ok(new List<GetCourseDto>());
            }

            var result = _courseService.GetSearchedCourses(searchText);

            if (result.Error)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Courses);
        }

        [HttpPut("publish/{id}")]
        public ActionResult PublishCourse(Guid id)
        {
            var result = _courseService.PublishCourse(id);

            if (!result.IsCoursePublished)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // Get Courses For the User which are not enrolled
        [HttpGet("published-courses/{userId}")]
        public ActionResult GetPublishedCourses(Guid userId)
        {
            var (result, message) = _courseService.GetPublishedCoursesForUser(userId);

            // there are no published courses available
            if (result.Count == 0)
            {
                return NotFound(message);
            }

            return Ok(result);
        }

        // Get courses that the user has enrolled


        [HttpPost("course-thumbnail")]
        public async Task<IActionResult> UploadCourseThumbnail(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // Only allow image types
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedTypes.Contains(file.ContentType))
                return BadRequest("Invalid file type");


            // Unique file name
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Full path to save the file
            var uploadPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads/Courses",
                fileName
            );

            // Save file
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative URL (frontend can use directly in <img>)
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var imageUrl = $"{baseUrl}/uploads/courses/{fileName}";

            return Ok(new { imageUrl });
        }
    }
}