using LMS.Api.Application.DTOs.Module;
using LMS.Api.Application.Errors;
using LMS.Api.Application.Services.Interfaces;
using LMS.Api.Domain.Entities;
using LMS.Api.Domain.Enums;
using LMS.Api.Infrastructure.Interfaces;

namespace LMS.Api.Application.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;

        public ModuleService(IModuleRepository moduleRepository, ICourseRepository courseRepository)
        {
            _moduleRepository = moduleRepository;
            _courseRepository = courseRepository;

        }

        public ModuleResponse CreateModule(Guid courseId, ModuleDto dto)
        {
            // Check if course exits
            var course = _courseRepository.Get(courseId);

            if (course is null)
            {
                return new ModuleResponse()
                {
                    IsModuleCreated = false,
                    Message = DomainErrors.ModuleErrors.CourseDoesNotExist()
                };
            }

            int order = _moduleRepository.GetOrder(course.Id);

            order++;

            var module = new Module()
            {
                Title = dto.Title,
                Description = dto.Description,
                VideoUrl = dto.VideoUrl,
                CourseId = course.Id,
                ModuleOrder = order,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var createdModule = _moduleRepository.Add(module);

            if (createdModule is null)
            {
                return new ModuleResponse()
                {
                    IsModuleCreated = false,
                    Message = DomainErrors.ModuleErrors.ModuleCreationFailed()
                };
            }

            return new ModuleResponse()
            {
                IsModuleCreated = true,
                Message = "Module Creation Successfull"
            };
        }

        public ModuleList GetModules(Guid courseId)
        {
            var modules = _moduleRepository.GetAll(courseId);

            if (modules?.Count == 0)
            {
                return new ModuleList()
                {
                    Message = DomainErrors.ModuleErrors.NoModules()
                };
            }

            List<GetModuleDto> moduleList = modules.Select(module => new GetModuleDto()
            {
                Id = module.Id,
                ModuleDto = new ModuleDto()
                {
                    Title = module.Title,
                    Description = module.Description,
                    VideoUrl = module.VideoUrl
                },
                Order = module.ModuleOrder
            }).ToList();

            return new ModuleList()
            {
                Modules = moduleList
            };
        }

        public ModuleResponse UpdateModule(Guid moduleId, ModuleDto dto)
        {
            var module = _moduleRepository.Get(moduleId);

            if (module is null)
            {
                return new ModuleResponse()
                {
                    IsModuleUpdated = false,
                    Message = DomainErrors.ModuleErrors.UpdatingModuleFailed()
                };
            }

            module.Title = dto.Title;
            module.Description = dto.Description;
            module.VideoUrl = dto.VideoUrl;
            module.UpdatedAt = DateTime.Now;

            var isModuleUpdated = _moduleRepository.Update(module);

            if (!isModuleUpdated)
            {
                return new ModuleResponse()
                {
                    IsModuleUpdated = false,
                    Message = DomainErrors.ModuleErrors.UpdatingModuleFailed()
                };
            }

            return new ModuleResponse()
            {
                IsModuleUpdated = true,
                Message = "Module Updated Successfully"
            };
        }

        public ModuleResponse DeleteModule(Guid moduleId)
        {
            // Get module
            var module = _moduleRepository.Get(moduleId);

            if (module == null)
            {
                return new ModuleResponse()
                {
                    IsModuleDeleted = false,
                    Message = DomainErrors.ModuleErrors.ModuleDoesNotExist()
                };
            }

            // Check if course is already published
            var course = _courseRepository.Get(module.CourseId);

            if (course.Status == CourseStatus.Published.ToString())
            {
                return new ModuleResponse()
                {
                    IsModuleDeleted = false,
                    Message = DomainErrors.ModuleErrors.CannotDeleteModuleDueToCourseAlreadyPublished()
                };
            }

            var modules = _moduleRepository.GetAll(course.Id);

            if (modules.Count == 1)
            {
                var isDeleted = _moduleRepository.Delete(module);

                if (!isDeleted)
                {
                    return new ModuleResponse()
                    {
                        IsModuleDeleted = false,
                        Message = DomainErrors.ModuleErrors.ModuleDeletionFailed()
                    };
                }

                return new ModuleResponse()
                {
                    IsModuleDeleted = true,
                    Message = "Module Deleted Successfully"
                };
            }

            // re-assign orders
            var moduleToDelete = module;
            Queue<Module> moduleOrdersToUpdate = new();

            foreach(var mod in modules)
            {
                if (mod.ModuleOrder > moduleToDelete.ModuleOrder)
                {
                    mod.ModuleOrder--;
                    moduleOrdersToUpdate.Enqueue(mod);
                }
            }

            if (moduleOrdersToUpdate.Count > 0)
            {
                bool success = _moduleRepository.UpdateBulk(moduleOrdersToUpdate);

                if (!success)
                {
                    return new ModuleResponse()
                    {
                        IsModuleDeleted = false,
                        Message = DomainErrors.ModuleErrors.ModuleReassignmentDuringDeletionFailed()
                    };
                }
            }

            bool isModuleDeleted = _moduleRepository.Delete(module);

            if (!isModuleDeleted)
            {
                return new ModuleResponse()
                {
                    IsModuleDeleted = false,
                    Message = DomainErrors.ModuleErrors.ModuleDeletionFailed()
                };
            }

            return new ModuleResponse()
            {
                IsModuleDeleted = true,
                Message = "Module Deleted Successfully"
            };
        }
    }
}