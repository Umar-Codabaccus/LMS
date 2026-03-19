using LMS.Api.Domain.Enums;
using LMS.Api.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Api.Domain.Entities;

[Table("Users")]
public sealed class User
{
    private readonly List<Course> _courses = [];
    private readonly List<EnrolledCourse> _enrolledCourses = [];
    private readonly List<UserRole> _userRoles = [];

    private User() { }

    public Guid Id { get; private init; }
    public string Firstname { get; private set; } = string.Empty;
    public string Lastname { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public string? ProfileImageUrl { get; private set; } = string.Empty;
    public string? Bio { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public IReadOnlyCollection<Course> Courses => _courses;
    public IReadOnlyCollection<EnrolledCourse> EnrolledCourses => _enrolledCourses;
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

    public static Result<User> RegisterUser(string firstname, string lastname, string email, string passwordHash, RoleType roleType)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = firstname,
            Lastname = lastname,
            Email = email,
            PasswordHash = passwordHash,
            Role = roleType.ToString(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return user;
    }

    public void Update(string firstname, string lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
        UpdatedAt = DateTime.UtcNow;
    }
}