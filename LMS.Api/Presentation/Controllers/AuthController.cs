using LMS.Api.Application.AuthServices.Login;
using LMS.Api.Application.AuthServices.Register;
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
        private readonly ILoginHandler _loginHandler;
        private readonly IRegisterUserHandler _registerUserHandler;

        public AuthController(
            IUserService userService,
            ILoginHandler loginHandler,
            IRegisterUserHandler registerUserHandler)
        {
            _userService = userService;
            _loginHandler = loginHandler;
            _registerUserHandler = registerUserHandler;
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
        public ActionResult JwtLogin(LoginRequest request)
        {
            var response = _loginHandler.Handle(request);

            if (response.IsFailure)
            {
                Error error = response.Error;
                switch (error.Type)
                {
                    case ErrorType.NotFound: return NotFound(response);
                    case ErrorType.BadRequest: return BadRequest(response);
                }
            }

            return Ok(response);
        }

        [HttpPost("jwt-register")]
        public async Task<ActionResult> JwtRegister(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            Result<RegisterUserResponse> response = await _registerUserHandler.Handle(request, cancellationToken);

            if (response.IsFailure && response.IsValidationError)
            {
                return BadRequest(response.Errors);
            }
            else if (response.IsFailure && !response.IsValidationError)
            {
                switch (response.Error.Type)
                {
                    case ErrorType.NotFound: return NotFound(response);
                    case ErrorType.BadRequest: return BadRequest(response);
                    case ErrorType.Conflict: return Conflict(response);
                }
            }

            return Created(nameof(JwtRegister), response);
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