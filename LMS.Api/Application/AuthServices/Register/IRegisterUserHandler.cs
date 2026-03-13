using LMS.Api.Shared;

namespace LMS.Api.Application.AuthServices.Register
{
    public interface IRegisterUserHandler
    {
        Result<RegisterUserResponse> Handle(RegisterUserRequest request);
    }
}