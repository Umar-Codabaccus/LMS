using Microsoft.EntityFrameworkCore;
using LMS.Api.Domain.Entities;

namespace LMS.Api.Infrastructure.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<EnrolledCourse> EnrolledCourses { get; set; }
    public DbSet<EnrolledModule> EnrolledModules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // EnrolledCourse → User (NO CASCADE)
        modelBuilder.Entity<EnrolledCourse>()
            .HasOne(e => e.User)
            .WithMany(u => u.EnrolledCourses)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // EnrolledCourse → Course (CASCADE is fine)
        modelBuilder.Entity<EnrolledCourse>()
            .HasOne(e => e.Course)
            .WithMany(c => c.EnrolledUsers)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Course → User (Instructor) (NO CASCADE)
        modelBuilder.Entity<Course>()
            .HasOne(c => c.User)
            .WithMany(u => u.Courses)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Module → Course (CASCADE)
        modelBuilder.Entity<Module>()
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .HasForeignKey(m => m.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
