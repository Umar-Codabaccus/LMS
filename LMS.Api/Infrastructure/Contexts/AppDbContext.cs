using Microsoft.EntityFrameworkCore;
using LMS.Api.Domain.Entities;
using LMS.Api.Application;

namespace LMS.Api.Infrastructure.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<PermissionGrant> PermissionGrants { get; set; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    //public async Task<int> BeginTransactionAsync(CancellationToken cancellationToken = default)
    //{
    //    return await base.BeginTransactionAsync(cancellationToken);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }

}
