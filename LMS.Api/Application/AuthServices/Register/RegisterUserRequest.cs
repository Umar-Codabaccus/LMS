namespace LMS.Api.Application.AuthServices.Register;

public sealed record RegisterUserRequest(string Firstname, string Lastname, string Email, string Password);
