using LMS.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Api.Application;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<EnrolledCourse> EnrolledCourses { get; set; }
    public DbSet<EnrolledModule> EnrolledModules { get; set; }
    Task<int> SaveChangesAsync();
}
