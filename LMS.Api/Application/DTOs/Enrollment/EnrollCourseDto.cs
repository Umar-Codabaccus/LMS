using LMS.Api.Application.DTOs.Module;

namespace LMS.Api.Application.DTOs.Enrollment
{
    public class EnrollModuleDto : ModuleDto
    {
        public Guid ModuleId { get; set; }
        public int Order { get; set; }
    }

    public class EnrollCourseDto : CourseDto
    {
        public Guid CourseId { get; set; }
        public string Status { get; set; }
        public List<EnrollModuleDto> EnrollModules { get; set; }
    }
}
