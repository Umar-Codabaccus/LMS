using LMS.Api.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Api.Domain.Entities;

public enum UserAccountType
{
    DefaultUserAccount = 0,
    DefaultInstructorAccount = 1,
    TutorOrgAccount = 2,
    StudentAccount = 3,
    AdminOrgAccount = 4,
    InstructorOrgAccount = 5,
    EmployeeOrgAccount = 6
}

[Table("Users")]
public sealed class User
{
    private readonly List<UserEmail> _userEmails = [];

    private User() { }

    public Guid Id { get; private init; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public IReadOnlyCollection<UserEmail> UserEmails => _userEmails;

    public static Result<User> Create(string firstname, string lastname)
    {
        // TODO: validate firstname and lastname

        var userId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;

        var user = new User()
        {
            Id = userId,
            Firstname = firstname,
            Lastname = lastname,
            CreatedAt = createdAt
        };

        return user;
    }
}
