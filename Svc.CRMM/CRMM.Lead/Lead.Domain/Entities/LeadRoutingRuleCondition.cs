namespace Lead.Domain.Entities;

public class LeadRoutingRuleCondition : BaseEntity
{
    public Guid LeadRoutingRuleId { get; set; }

    public LeadRoutingRule? LeadRoutingRule { get; set; }

    public int SortOrder { get; set; }

    public string Field { get; set; } = default!;

    public string Operator { get; set; } = default!;

    public string Value { get; set; } = default!;
}
