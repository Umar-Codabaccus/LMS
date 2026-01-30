namespace LMS.Api.Application.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
    }
}
