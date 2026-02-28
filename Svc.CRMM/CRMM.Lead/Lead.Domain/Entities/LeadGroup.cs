namespace Lead.Domain.Entities;

public class LeadGroup : BaseEntity
{
    public string Code { get;set; } = default!;
    public string Name { get;set; } = default!;
    public bool IsActive { get;set; }
    public DateTime CreatedAt { get;set; }
    public DateTime UpdatedAt { get;set; }

    public List<LeadGroupCondition> Conditions { get;set; } = new();
}
