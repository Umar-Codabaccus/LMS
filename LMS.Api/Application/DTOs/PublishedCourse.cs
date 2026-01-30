namespace LMS.Api.Application.DTOs
{
    public class PublishedCourse
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
    }
}