using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class UserRole
{
    private UserRole() { }

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public User User { get; private set; }
    public Role Role { get; private set; }

    public static Result<UserRole> Create(Guid userId, Guid roleId)
    {
        var userRole = new UserRole()
        {
            UserId = userId,
            RoleId = roleId
        };

        return userRole;
    }
}
