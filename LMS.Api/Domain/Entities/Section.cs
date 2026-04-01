using LMS.Api.Shared;

namespace LMS.Api.Domain.Entities;

public sealed class Section
{
    private readonly List<Permission> _permissions = [];

    private Section() { }

    public Guid Id { get; private init; }
    public string SectionName { get; private set; }
    public Guid OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    public IReadOnlyCollection<Permission> Permissions => _permissions;

    public static Result<Section> Create(string sectionName)
    {
        // TODO: validate section name

        var section = new Section()
        {
            Id = Guid.NewGuid(),
            SectionName = sectionName
        };

        return section;
    }
}