using Azure.Core;
using LMS.Api.Application.DTOs.Auth;
using LMS.Api.Application.Errors;
using LMS.Api.Application.Services.Interfaces;
using LMS.Api.Infrastructure.Authentication;
using LMS.Api.Infrastructure.Interfaces;
using LMS.Api.Shared;

namespace LMS.Api.Application.Services;

public sealed class AuthService(IUserRepository userRepository, ITokenProvider tokenProvider) : IAuthService
{
    public Result<string> LoginUser(AuthRequest request)
    {
        // Check if user account exists
        var user = userRepository.CheckExistingAccount(request.Email);

        if (user is null)
        {
            // return account does not exist error
            Error error = new()
            {
                Code = nameof(DomainErrors.AuthErrors.AccountDoesNotExist),
                Message = DomainErrors.AuthErrors.AccountDoesNotExist(),
                Type = ErrorType.NotFound
            };

            return Result.Failure<string>(error);
        }

        // Check if password matches
        bool isPasswordCorrect = PasswordHasher.VerifyPassword(request.Password, user.PasswordHash);

        if (isPasswordCorrect == false)
        {
            // return wrong password error
            Error error = new()
            {
                Code = nameof(DomainErrors.AuthErrors.WrongPassword),
                Message = DomainErrors.AuthErrors.WrongPassword(),
                Type = ErrorType.BadRequest
            };

            return Result.Failure<string>(error);
        }

        string token = tokenProvider.Create(user);

        return Result.Success(token);
    }
}
