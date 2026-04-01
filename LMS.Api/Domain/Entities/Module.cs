using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Api.Domain.Entities;

[Table("Modules")]
public sealed class Module
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CourseId { get; set; } // Connects to the Course table
    public string Title { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ModuleOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Course? Course { get; set; }
    public EnrolledModule EnrolledModule { get; set; }
}
