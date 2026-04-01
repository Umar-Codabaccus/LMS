using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class Permission
{
    private readonly List<RolePermission> _permittedRoles = [];
    private Permission() { }

    public Guid Id { get; private init; }
    public string PermissionName { get; private set; }
    public Guid SectionId { get; private set; }
    public Section? Section { get; private set; }

    public Guid OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    public IReadOnlyCollection<RolePermission> PermittedRoles => _permittedRoles;

    public static Result<Permission> Create(string permissionName)
    {
        // TODO: validate permissionName

        var permission = new Permission()
        {
            Id = Guid.NewGuid(),
            PermissionName = permissionName
        };

        return permission;
    }
}
