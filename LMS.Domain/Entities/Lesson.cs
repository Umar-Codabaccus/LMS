using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities;

[Table("Lessons")]
public sealed class Lesson
{
    [Key]
    [Column("LessonId", TypeName = "nvarchar(500)")]
    public string Id { get; set; } = string.Empty;

    [Required]
    [Column("ModuleId", TypeName = "nvarchar(500)")]
    [ForeignKey("Module")]
    public string ModuleId { get; set; } = string.Empty; // Connects to the Module table

    [Required]
    [Column("Title", TypeName = "nvarchar(150)")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("VideoUrl", TypeName = "nvarchar(255)")]
    public string VideoUrl { get; set; } = string.Empty;

    [Column("Content", TypeName = "nvarchar(max)")]
    public string Content { get; set; } = string.Empty;

    [Required]
    public int LessonOrder { get; set; }

    public int DurationMinutes { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Module? Module { get; set; }
}
