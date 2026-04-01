using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class UserEmail
{
    private readonly List<UserAccount> _userAccounts = [];

    private UserEmail() { }

    public Guid Id { get; private init; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public bool IsDefault { get; private set; }

    public Guid UserId { get; private init; }
    public User? User { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public IReadOnlyCollection<UserAccount> UserAccounts => _userAccounts;

    public static Result<UserEmail> Create(Guid userId, string email, string hashedPassword)
    {
        // todo: validate email and password
        Guid id = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;

        var userEmail = new UserEmail()
        {
            Id = id,
            UserId = userId,
            Email = email,
            HashedPassword = hashedPassword,
            IsDefault = false,
            CreatedAt = createdAt
        };

        return userEmail;
    }
}
