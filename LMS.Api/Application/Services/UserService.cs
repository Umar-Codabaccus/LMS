using LMS.Api.Application.DTOs.Auth;
using LMS.Api.Infrastructure.Interfaces;
using LMS.Api.Domain.Entities;
using LMS.Api.Application.Utilities;
using LMS.Api.Application.Errors;
using LMS.Api.Domain.Enums;
using LMS.Api.Application.Services.Interfaces;
using LMS.Api.Application.DTOs.User;

namespace LMS.Api.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthResponse Register(RegisterDto dto)
        {
            AuthResponse authResponse = new();

            // Check if user exists
            var userAccount = _userRepository.CheckExistingAccount(dto.Email);

            if (userAccount is not null)
            {
                return new AuthResponse()
                {
                    IsAuthenticated = false,
                    Message = DomainErrors.AuthErrors.AccountAlreadyExists()
                };
            }

            // Hash Password
            var passwordHash = PasswordHelper.HashPassword(dto.Password);

            // Create user
            User user = new()
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Email = dto.Email,
                Role = RoleType.Learner.ToString(),
                PasswordHash = passwordHash
            };

            // Add User to database
            var addedUser = _userRepository.Add(user);

            if (addedUser is null)
            {
                authResponse = new()
                {
                    IsAuthenticated = false,
                    Message = DomainErrors.AuthErrors.RegistrationFailed()
                };
            }
            else
            {
                authResponse = new()
                {
                    UserId = addedUser.Id,
                    Email = addedUser.Email,
                    Role = addedUser.Role,
                    IsAuthenticated = true,
                    Message = "Registration Successfull"
                };
            }

            return authResponse;
        }

        public AuthResponse Login(AuthRequest request)
        {
            // Check if user account exists
            var userAccount = _userRepository.CheckExistingAccount(request.Email);

            if (userAccount is null)
            {
                // return account does not exist error
                return new AuthResponse()
                {
                    IsAuthenticated = false,
                    Message = DomainErrors.AuthErrors.AccountDoesNotExist()
                };
            }

            // Check if password matches
            bool isPasswordCorrect = PasswordHelper.VerifyPassword(request.Password, userAccount.PasswordHash);

            if (isPasswordCorrect == false)
            {
                // return wrong password error
                return new AuthResponse()
                {
                    IsAuthenticated = false,
                    Message = DomainErrors.AuthErrors.WrongPassword()
                };
            }

            return new AuthResponse()
            {
                UserId = userAccount.Id,
                Email = userAccount.Email,
                Role = userAccount.Role,
                IsAuthenticated = true,
                Message = "Login Successful"
            };
        }

        public UserListDto GetAllUsers()
        {
            var users = _userRepository.GetAll();

            if (users?.Count == 0)
            {
                return new UserListDto
                {
                    Message = DomainErrors.UserErrors.NoUsers()
                };
            }

            List<UserDto> userDtos = users
                .Select(user => new UserDto
                    {
                        Id = user.Id,
                        Fullname = user.Firstname + " " + user.Lastname,
                        Email = user.Email,
                        Role = user.Role,
                        ProfileUrl = user.ProfileImageUrl
                    })
                .ToList();

            return new UserListDto()
            {
                Users = userDtos,
                Message = "Users accessed successfull"
            };
        }

        public UpdateUserResponse UpdateUser(Guid id, UpdateUserDto dto)
        {
            var user = _userRepository.Get(id);

            if (user is null)
            {
                return new UpdateUserResponse()
                {
                    IsUpdated = false,
                    Message = DomainErrors.AuthErrors.AccountDoesNotExist()
                };
            }

            user.Firstname = dto.Firstname;
            user.Lastname = dto.Lastname;
            user.UpdatedAt = DateTime.Now;

            // Update user
            var isUserUpdated = _userRepository.Update(user);

            if (!isUserUpdated)
            {
                return new UpdateUserResponse()
                {
                    IsUpdated = false,
                    Message = DomainErrors.UserErrors.CouldNotUpdateUser()
                };
            }

            return new UpdateUserResponse()
            {
                IsUpdated = true,
                Message = "Update successful"
            };
        }

        public string DeleteUser(Guid id)
        {
            var user = _userRepository.Get(id);

            if (user is null)
            {
                return DomainErrors.AuthErrors.AccountDoesNotExist();
            }

            var isUserDeleted = _userRepository.Delete(user);

            if (!isUserDeleted)
            {
                return DomainErrors.UserErrors.CouldNotDeleteUser();
            }

            return "Deletion Successfull";
        }

        public UserListDto GetUsersBySearch(string search)
        {
            var users = _userRepository.GetUsersBySearch(search);

            if (users?.Count == 0)
            {
                return new UserListDto
                {
                    Message = DomainErrors.UserErrors.NoUsers()
                };
            }

            List<UserDto> userDtos = users
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Fullname = user.Firstname + " " + user.Lastname,
                    Email = user.Email,
                    Role = user.Role,
                    ProfileUrl = user.ProfileImageUrl
                })
                .ToList();

            return new UserListDto()
            {
                Users = userDtos,
                Message = "Users accessed successfull"
            };
        }
        public GetUserIdDto GetUserIdByEmail(string email)
        {
            var user = _userRepository.CheckExistingAccount(email);

            if (user is null)
                return new GetUserIdDto() { Success = false, Message = DomainErrors.AuthErrors.AccountDoesNotExist() };

            return new GetUserIdDto() { Id = user.Id, Success = true, Message = "Account fetched successfully"};
        }
    }
}