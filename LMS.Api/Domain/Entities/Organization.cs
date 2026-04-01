using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

/// <summary>
/// Organization is an entity to represent companies, schools and orgs that are willing to use the lms platform to offer courses to the public, to their own internal training.
/// 
/// 
/// Organization can be a tuition type based where an instructor can register as a tuition-type organization where the tutor can upload courses that can be accessed by this own registered students
/// </summary>

public sealed class Organization
{
    private Organization() { }

    public Guid Id { get; private init; }
    public string OrganizationName { get; private set; }

    // FIRST BATCH TYPE --> Company (Public/Private), Governments, SECOND --> Tuition
    public string OrganizationType { get; private set; } 

    public static Result<Organization> Create(string organizationName, string organizationType)
    {
        var organization = new Organization()
        {
            Id = Guid.NewGuid(),
            OrganizationName = organizationName,
            OrganizationType = organizationType
        };

        return organization;
    }
}
