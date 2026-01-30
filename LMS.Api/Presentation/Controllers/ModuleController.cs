using LMS.Api.Application.DTOs.Module;
using LMS.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpPost("{courseId}/create")]
        public ActionResult CreateModule(Guid courseId, ModuleDto dto)
        {
            var result = _moduleService.CreateModule(courseId, dto);

            if (!result.IsModuleCreated)
            {
                return BadRequest(result.Message);
            }

            return Created();
        }

        [HttpPut("update/{id}")]
        public ActionResult UpdateModule(Guid id, ModuleDto dto)
        {
            var result = _moduleService.UpdateModule(id, dto);

            if (!result.IsModuleUpdated)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpGet("{courseId}/modules")]
        public ActionResult<ModuleList> GetModules(Guid courseId)
        {
            var result = _moduleService.GetModules(courseId);

            if (result.Modules == null)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("delete/{moduleId}")]
        public ActionResult DeleteModule(Guid moduleId)
        {
            var result = _moduleService.DeleteModule(moduleId);

            if (!result.IsModuleDeleted)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}