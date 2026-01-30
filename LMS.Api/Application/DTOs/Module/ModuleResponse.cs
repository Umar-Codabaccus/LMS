namespace LMS.Api.Application.DTOs.Module
{
    public class ModuleResponse
    {
        public bool IsModuleCreated { get; set; }
        public bool IsModuleUpdated { get; set; }
        public bool IsModuleDeleted { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
