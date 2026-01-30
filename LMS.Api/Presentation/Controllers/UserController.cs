using LMS.Api.Application.DTOs.User;
using LMS.Api.Application.Errors;
using LMS.Api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public ActionResult<UserListDto> GetUsers()
        {
            var result = _userService.GetAllUsers();

            if (result.Message == DomainErrors.UserErrors.NoUsers())
            {
                return NotFound("There are no users");
            }

            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public ActionResult UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
        {
            var result = _userService.UpdateUser(id, dto);

            if (result.Message == DomainErrors.AuthErrors.AccountDoesNotExist())
            {
                return BadRequest("Could not find user");
            }

            if (result.Message == DomainErrors.UserErrors.CouldNotUpdateUser())
            {
                return BadRequest("Could not update user");
            }

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            var result = _userService.DeleteUser(id);

            if (result == DomainErrors.AuthErrors.AccountDoesNotExist())
            {
                return BadRequest("Could not find user");
            }

            if (result == DomainErrors.UserErrors.CouldNotDeleteUser())
            {
                return BadRequest("Could not delete user");
            }

            return NoContent();
        }

        [HttpGet("users/search")]
        public ActionResult SearchUsers([FromQuery] string search)
        {
            var result = _userService.GetUsersBySearch(search);

            if (result.Message == DomainErrors.UserErrors.NoUsers())
            {
                return NotFound("No user found");
            }

            return Ok(result);
        }

        // Get User By Email
        [HttpGet("user/email")]
        public ActionResult GetUserIdByEmail([FromQuery] string email)
        {
            var result = _userService.GetUserIdByEmail(email);

            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }
    }
}
