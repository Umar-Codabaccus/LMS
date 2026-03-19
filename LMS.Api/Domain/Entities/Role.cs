using LMS.Api.Domain.Enums;
using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class Role
{
    private List<UserRole> _userRoles = [];
    private Role() { }

    public Guid Id { get; private init; }
    public RoleType Type { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

    public static Result<Role> Create(RoleType type)
    {
        var role = new Role()
        {
            Id = Guid.NewGuid(),
            Type = type
        };

        return role;
    }
}
