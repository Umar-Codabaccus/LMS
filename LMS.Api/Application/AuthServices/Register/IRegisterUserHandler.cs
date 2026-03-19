using LMS.Api.Shared;

namespace LMS.Api.Application.AuthServices.Register
{
    public interface IRegisterUserHandler
    {
        Task<Result<RegisterUserResponse>> Handle(RegisterUserRequest request);
    }
}