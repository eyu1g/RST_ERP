namespace Lead.Domain.Entities;

public sealed class LeadAssignmentRule
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private LeadAssignmentRule() { }

    public LeadAssignmentRule(Guid id, string name, bool isActive)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
}
