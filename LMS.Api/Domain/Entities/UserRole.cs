using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class UserRole
{
    private UserRole() { }

    public Guid UserAccountId { get; private init; }
    public Guid RoleId { get; private init; }

    public UserAccount? UserAccount { get; private set; }
    public Role? Role { get; private set; }

    public static Result<UserRole> Create(Guid userId, Guid roleId)
    {
        var userRole = new UserRole()
        {
            UserAccountId = userId,
            RoleId = roleId
        };

        return userRole;
    }
}
