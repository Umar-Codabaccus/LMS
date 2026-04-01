using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class Role
{
    private readonly List<UserRole> _userRoles = [];
    private readonly List<RolePermission> _permissions = [];

    private Role() { }
    
    public Guid Id { get; private init; }
    public string RoleName { get; private set; }

    // roles defined within an organization
    public Guid OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;
    public IReadOnlyCollection<RolePermission> Permissions => _permissions;

    public static Result<Role> Create(string roleName)
    {
        // TODO: validate rolename
        var role = new Role()
        {
            Id = Guid.NewGuid(),
            RoleName = roleName
        };

        return role;
    }
}
