namespace LMS.Api.Application.DTOs.User
{
    public class UserListDto
    {
        public List<UserDto>? Users { get; set; }
        public string Message = string.Empty;
    }
}
