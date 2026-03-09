using LMS.Api.Shared;

namespace LMS.Api.Application.DTOs.Auth;

public sealed record JwtAuthResponse(string Token, Error error);
