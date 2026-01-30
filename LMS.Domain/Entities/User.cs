using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities;

[Table("Users")]
public sealed class User
{
    [Key]
    [Column("UserId", TypeName = "nvarchar(500)")]
    [MaxLength(500)]
    [StringLength(500)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [StringLength(50, MinimumLength = 2)]
    [Column("Firstname", TypeName = "nvarchar(50)")]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [StringLength(50, MinimumLength = 2)]
    [Column("Lastname", TypeName = "nvarchar(50)")]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [StringLength(150)]
    [Column("Email", TypeName = "nvarchar(150)")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column("Password", TypeName = "nvarchar(255)")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [Column("Role", TypeName = "nvarchar(50)")]
    public string Role { get; set; } = string.Empty;

    [Column("ProfileImageUrl", TypeName = "nvarchar(255)")]
    public string ProfileImageUrl { get; set; } = string.Empty;

    [Column("Bio", TypeName = "nvarchar(500)")]
    public string Bio { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Course> Courses { get; set; }
}