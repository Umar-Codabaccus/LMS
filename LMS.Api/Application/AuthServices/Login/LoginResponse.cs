using LMS.Api.Shared;

namespace LMS.Api.Application.AuthServices.Login;

public sealed record LoginResponse(string Token, Error Error);
