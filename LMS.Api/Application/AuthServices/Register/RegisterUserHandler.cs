using LMS.Api.Application.Utilities;
using LMS.Api.Domain.Entities;
using LMS.Api.Domain.Enums;
using LMS.Api.Shared;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
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

        var email = context.Users
            .Where(u => u.Email == request.Email)
            .Select(u => u.Email)
            .FirstOrDefault();

        if (email is not null)
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
        var user = new User()
        {
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Email = request.Email,
            Role = RoleType.Learner.ToString(),
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Users.Add(user);

        context.SaveChanges();

        var token = tokenProvider.Create(user);

        var response = new RegisterUserResponse(token, Error.None);

        return Result.Success(response);
    }
}
