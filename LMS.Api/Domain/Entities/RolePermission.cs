using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class RolePermission
{
    private RolePermission() { }

    public Guid RoleId { get; private init; }
    public Guid PermissionId { get; private init; }
    public Guid OrganizationId { get; private init; }

    public Role? Role { get; private set; }
    public Permission? Permission { get; private set; }
    public Organization? Organization { get; private set; }

    public static Result<RolePermission> Create(Guid roleId, Guid permissionId)
    {
        var rolePermission = new RolePermission()
        {
            RoleId = roleId,
            PermissionId = permissionId
        };

        return rolePermission;
    }
}
