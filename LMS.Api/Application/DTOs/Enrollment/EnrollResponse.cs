namespace LMS.Api.Application.DTOs.Enrollment
{
    public class EnrollResponse
    {
        public Guid EnrolledCourseId { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public List<EnrolledModuleDto>? Modules { get; set; }
    }

    public class EnrolledModuleDto
    {
        public Guid EnrolledModuleId { get; set; }
        public Guid ModuleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public int Order { get; set; }
    }
}
