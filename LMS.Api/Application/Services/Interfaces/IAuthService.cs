using LMS.Api.Application.DTOs.Auth;
using LMS.Api.Shared;

namespace LMS.Api.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Result<string> LoginUser(AuthRequest request);
    }
}