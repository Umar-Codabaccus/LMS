namespace LMS.Api.Application.DTOs.User
{
    public class GetUserIdDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
