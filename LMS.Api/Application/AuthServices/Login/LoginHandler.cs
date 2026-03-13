using LMS.Api.Application.Errors;
using LMS.Api.Application.Utilities;
using LMS.Api.Infrastructure.Authentication;
using LMS.Api.Shared;

namespace LMS.Api.Application.AuthServices.Login;

public sealed class LoginHandler(IAppDbContext context, ITokenProvider tokenProvider) : ILoginHandler
{
    public Result<LoginResponse> Handle(LoginRequest request)
    {
        // get user
        var user = context.Users.FirstOrDefault(user => user.Email == request.Email);

        if (user is null)
        {
            Error error = new()
            {
                Code = nameof(DomainErrors.AuthErrors.AccountDoesNotExist),
                Message = DomainErrors.AuthErrors.AccountDoesNotExist(),
                Type = ErrorType.NotFound
            };

            return Result.Failure<LoginResponse>(error);
        }

        // check password
        if (!PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
        {
            Error error = new()
            {
                Code = nameof(DomainErrors.AuthErrors.WrongPassword),
                Message = DomainErrors.AuthErrors.WrongPassword(),
                Type = ErrorType.BadRequest
            };

            return Result.Failure<LoginResponse>(error);
        }

        string token = tokenProvider.Create(user);

        var response = new LoginResponse(token, Error.None);

        return Result.Success(response);
    }
}
