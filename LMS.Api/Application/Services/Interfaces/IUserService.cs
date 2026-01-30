using LMS.Api.Application.DTOs.Auth;
using LMS.Api.Application.DTOs.User;

namespace LMS.Api.Application.Services.Interfaces
{
    public interface IUserService
    {
        public AuthResponse Register(RegisterDto dto);
        public AuthResponse Login(AuthRequest request);
        public UserListDto GetAllUsers();
        public UpdateUserResponse UpdateUser(Guid id, UpdateUserDto dto);
        public string DeleteUser(Guid id);
        public UserListDto GetUsersBySearch(string search);
        public GetUserIdDto GetUserIdByEmail(string email);
    }
}
