using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

/// <summary>
/// USERS --> ROLES --> PERMISSIONS --> SECTIONS
/// 
/// ROLE --> Admin
/// SECTION --> user
/// PERMISSION --> revoke_subscription
/// 
/// 
/// a user can have direct permissions, and can have temporary permissions
/// </summary>


/// <summary>
/// Permission is granted either to a specific user or a role
/// Do not create a PermissionGrant object with both user_id and role_id.
/// </summary>
public sealed class PermissionGrant
{
    private PermissionGrant() { }

    public Guid Id { get; private init; }
    public Guid? UserId { get; private init; }
    public Guid? RoleId { get; private init; }
    public Guid PermissionId { get; private init; }
    public Permission? Permission { get; private set; }

    public Guid OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    public DateTime GrantedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }


    public static Result<PermissionGrant> GrantPermissionToUser(Guid userId, Guid permissionId, DateTime expiresAt)
    {
        var permissionGrant = new PermissionGrant()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PermissionId = permissionId,
            GrantedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt
        };

        return permissionGrant;
    }

    public static Result<PermissionGrant> GrantPermissionToRole(Guid roleId, Guid permissionId, DateTime expiresAt)
    {
        var permissionGrant = new PermissionGrant()
        {
            Id = Guid.NewGuid(),
            RoleId = roleId,
            PermissionId = permissionId,
            GrantedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt
        };

        return permissionGrant;
    }
}
