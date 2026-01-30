using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities;

[Table("Module")]
public sealed class Module
{
    [Key]
    [Column("ModuleId", TypeName = "nvarchar(500)")]
    public string Id { get; set; } = string.Empty;

    [Required]
    [Column("CourseId", TypeName = "nvarchar(500)")]
    [ForeignKey("Course")]
    public string CourseId { get; set; } = string.Empty; // Connects to the Course table

    [Required]
    [Column("Title", TypeName = "nvarchar(150)")]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int ModuleOrder { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Course? Course { get; set; }

    public List<Lesson> Lessons { get; set; }
}
