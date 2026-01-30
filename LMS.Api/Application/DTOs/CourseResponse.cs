namespace LMS.Api.Application.DTOs
{
    public class CourseResponse
    {
        public bool IsCourseCreated { get; set; }
        public bool IsCourseUpdated { get; set; }
        public bool IsCourseDeleted { get; set; }
        public bool IsCoursePublished { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
