namespace Lead.Domain.Entities;

public sealed class LeadGroup
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public List<LeadGroupCondition> Conditions { get; private set; } = new();

    private LeadGroup() { }

    public LeadGroup(Guid id, string code, string name, bool isActive, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        Id = id;
        Code = code;
        Name = name;
        IsActive = isActive;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public void Update(string code, string name, bool isActive, DateTimeOffset updatedAt)
    {
        Code = code;
        Name = name;
        IsActive = isActive;
        UpdatedAt = updatedAt;
    }
}
