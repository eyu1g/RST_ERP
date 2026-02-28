namespace Lead.Domain.Entities;

public class LeadGroupCondition : BaseEntity
{
    public Guid LeadGroupId { get;set; }
    public LeadGroup? LeadGroup { get;set; }

    public int SortOrder { get;set; }

    public string Field { get;set; } = default!;
    public string Operator { get;set; } = default!;
    public string Value { get;set; } = default!;

    public DateTime CreatedAt { get;set; }
    public DateTime UpdatedAt { get;set; }
}
