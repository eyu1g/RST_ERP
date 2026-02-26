namespace Lead.Domain.Entities;

public sealed class LeadStatus
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }

    private LeadStatus() { }

    public LeadStatus(Guid id, string name, bool isActive, int sortOrder)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
        SortOrder = sortOrder;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
}
