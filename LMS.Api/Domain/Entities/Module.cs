using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Api.Domain.Entities;

[Table("Modules")]
public sealed class Module
{
    [Key]
    [Column("ModuleId")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("CourseId")]
    [ForeignKey("Course")]
    public Guid CourseId { get; set; } // Connects to the Course table

    [Required]
    [Column("Title", TypeName = "nvarchar(150)")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("VideoUrl", TypeName = "nvarchar(255)")]
    public string VideoUrl { get; set; } = string.Empty;

    [Required]
    [Column("Description", TypeName = "nvarchar(max)")]
    public string Description { get; set; } = string.Empty;

    public int ModuleOrder { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Course? Course { get; set; }
    public EnrolledModule EnrolledModule { get; set; }
}
