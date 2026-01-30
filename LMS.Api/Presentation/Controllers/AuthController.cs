using LMS.Api.Application.DTOs.Auth;
using LMS.Api.Application.Errors;
using LMS.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult<AuthResponse> Register(RegisterDto dto)
        {
            var result = _userService.Register(dto);

            // if user already exists
            if (result.Message == DomainErrors.AuthErrors.AccountAlreadyExists())
            {
                return Conflict(DomainErrors.AuthErrors.AccountAlreadyExists());
            }

            // if registration failed
            if (result.Message == DomainErrors.AuthErrors.RegistrationFailed())
            {
                return BadRequest(DomainErrors.AuthErrors.RegistrationFailed());
            }

            return Created(string.Empty, result);
        }

        [HttpPost("login")]
        public ActionResult<AuthResponse> Login(AuthRequest request)
        {
            var result = _userService.Login(request);

            if (result.Message == DomainErrors.AuthErrors.AccountDoesNotExist())
            {
                return NotFound(DomainErrors.AuthErrors.AccountDoesNotExist());
            }

            if (result.Message == DomainErrors.AuthErrors.WrongPassword())
            {
                return BadRequest(DomainErrors.AuthErrors.WrongPassword());
            }

            return Ok(result);
        }
    }
}