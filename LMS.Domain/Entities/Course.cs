using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities;

[Table("Courses")]
public sealed class Course
{
    [Key]
    [Column("CourseId", TypeName = "nvarchar(500)")]
    public string Id { get; set; } = string.Empty;

    [Required]
    [Column("Title", TypeName = "nvarchar(50)")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("Description", TypeName = "nvarchar(max)")]
    public string Description { get; set; } = string.Empty;

    [Column("Category", TypeName = "nvarchar(50)")]
    public string Category { get; set; } = string.Empty;

    [Required]
    [Column("Level", TypeName = "nvarchar(50)")]
    public string Level { get; set; } = string.Empty;

    [Required]
    [Column("ThumbnailUrl", TypeName = "nvarchar(255)")]
    public string ThumbnailUrl { get; set; } = string.Empty;

    public DateTime PublishedDate { get; set; }

    [Required]
    [Column("Status", TypeName = "nvarchar(50)")]
    public string Status { get; set; } = string.Empty;

    public decimal Duration { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [Required]
    [Column("InstructorId", TypeName = "nvarchar(255)")]
    [ForeignKey("User")]
    public string InstructorId { get; set; } = string.Empty; // Connects to the User table

    public User? User { get; set; }

    public List<Module> Modules { get; set; }
}
