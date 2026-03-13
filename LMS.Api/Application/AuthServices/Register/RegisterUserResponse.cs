using LMS.Api.Shared;

namespace LMS.Api.Application.AuthServices.Register;

public sealed record RegisterUserResponse(string Token, Error Error);