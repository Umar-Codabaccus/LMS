namespace LMS.Api.Application.DTOs.Module
{
    public class GetModuleDto
    {
        public Guid Id { get; set; }
        public ModuleDto ModuleDto { get; set; }
        public int Order { get; set; }
    }
}
