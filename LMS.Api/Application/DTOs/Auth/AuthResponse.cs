namespace LMS.Api.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}