using LMS.Api.Shared;

namespace LMS.Api.Application.AuthServices.Login
{
    public interface ILoginHandler
    {
        Result<LoginResponse> Handle(LoginRequest request);
    }
}