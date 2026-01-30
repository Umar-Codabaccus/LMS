namespace LMS.Api.Application.DTOs
{
    public class CourseList
    {
        public List<GetCourseDto>? Courses { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
