namespace Lead.Domain.Entities;

public sealed class LeadSource
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private LeadSource() { }

    public LeadSource(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
