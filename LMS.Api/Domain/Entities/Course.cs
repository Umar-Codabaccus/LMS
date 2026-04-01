using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Api.Domain.Entities;

[Table("Courses")]
public sealed class Course
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; } = string.Empty;
    public DateTime? PublishedDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Guid InstructorId { get; set; } // Connects to the User table
    public User? User { get; set; }
    public List<Module> Modules { get; set; }
    public List<EnrolledCourse> EnrolledUsers { get; set; }
}
