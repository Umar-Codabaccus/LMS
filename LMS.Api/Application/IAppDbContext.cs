using LMS.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Api.Application;

public interface IAppDbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<PermissionGrant> PermissionGrants { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}