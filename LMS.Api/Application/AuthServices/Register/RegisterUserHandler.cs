using LMS.Api.Application.Utilities;
using LMS.Api.Domain.Entities;
using LMS.Api.Domain.Enums;
using LMS.Api.Shared;
using LMS.Api.Application.Errors;

namespace LMS.Api.Application.AuthServices.Register;

public sealed class RegisterUserHandler(ITokenProvider tokenProvider, IAppDbContext context) : IRegisterUserHandler
{
    public Result<RegisterUserResponse> Handle(RegisterUserRequest request)
    {
        ValidationErrors validationErrors = RegisterUserValidator.Validate(request);

        if (validationErrors.Errors.Any())
        {
            return Result.Failure<RegisterUserResponse>(validationErrors);
        }

        var userExist = context.Users
            .Any(u => u.Email == request.Email);

        if (userExist)
        {
            return Result.Failure<RegisterUserResponse>(
                new Error()
                {
                    Code = DomainErrors.AuthErrors.AccountAlreadyExists(),
                    Message = "User account already exists",
                    Type = ErrorType.Conflict
                });
        }

        var passwordHash = PasswordHelper.HashPassword(request.Password);
        var userResult = User.RegisterUser(
                request.Firstname,
                request.Lastname,
                request.Email,
                passwordHash,
                RoleType.Learner
            );

        var user = userResult.Value;

        if (user is null)
        {
            return Result.Failure<RegisterUserResponse>(
                new Error()
                {
                    Code = "UserRegistrationFailed",
                    Message = "Failed to register user",
                    Type = ErrorType.Internal
                });
        }

        context.Users.Add(user);

        context.SaveChanges();

        var token = tokenProvider.Create(user);

        var response = new RegisterUserResponse(token, Error.None);

        return Result.Success(response);
    }
}
