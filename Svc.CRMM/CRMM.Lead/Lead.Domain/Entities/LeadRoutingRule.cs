namespace Lead.Domain.Entities;

public class LeadRoutingRule : BaseEntity
{
    public int Priority { get; set; }

    public string Name { get; set; } = default!;

    public Guid? AssignToUserId { get; set; }

    public string? AssignToName { get; set; }

    public bool IsActive { get; set; }

    public List<LeadRoutingRuleCondition> Conditions { get; set; } = new();
}
