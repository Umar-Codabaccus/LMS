namespace LMS.Api.Application.DTOs.Module
{
    public class ModuleList
    {
        public List<GetModuleDto> Modules { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
