using LMS.Api.Application.DTOs.Module;

namespace LMS.Api.Application.Services.Interfaces
{
    public interface IModuleService
    {
        public ModuleResponse CreateModule(Guid courseId, ModuleDto dto);
        public ModuleResponse UpdateModule(Guid moduleId, ModuleDto dto);
        public ModuleList GetModules(Guid courseId);
        public ModuleResponse DeleteModule(Guid moduleId);
    }
}
