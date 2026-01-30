namespace LMS.Api.Application.DTOs
{
    public class GetCourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
