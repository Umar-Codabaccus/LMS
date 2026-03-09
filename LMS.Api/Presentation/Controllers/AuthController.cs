using LMS.Api.Application.DTOs.Auth;
using LMS.Api.Application.Errors;
using LMS.Api.Application.Services.Interfaces;
using LMS.Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
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

        [HttpPost("jwt-login")]
        public ActionResult JwtLogin(AuthRequest request)
        {
            var result = _authService.LoginUser(request);

            var response = new JwtAuthResponse(result.Value, result.Error);

            if (result.IsFailure)
            {
                Error error = result.Error;
                switch (error.Type)
                {
                    case ErrorType.NotFound: return NotFound(response);
                    case ErrorType.BadRequest: return BadRequest(response);
                }
            }

            return Ok(response);
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