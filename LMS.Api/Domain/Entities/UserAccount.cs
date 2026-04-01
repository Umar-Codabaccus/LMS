using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class UserAccount
{
    private readonly List<UserRole> _userRoles = [];

    private UserAccount() { }

    public Guid Id { get; private init; }

    // Public Account (Public Instructor or General Learner), SaaS Administrator, Private Account (Organization Employee or Private Tutor/Registered Students)
    public string AccountType { get; private set; } 
    public bool IsArchived { get; private set; }

    public Guid EmailId { get; private set; }
    public UserEmail? UserEmail { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Guid? OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;
    
    public static Result<UserAccount> Create(Guid organizationId, Guid emailId, string accountType)
    {
        // TODO: validate account type
        Guid userAccountId = Guid.NewGuid();
        DateTime createdAt = DateTime.UtcNow;

        var userAccount = new UserAccount()
        {
            Id = userAccountId,
            OrganizationId = organizationId,
            EmailId = emailId,
            AccountType = accountType,
            IsArchived = false
        };

        return userAccount;
    }
}