namespace Lead.Domain.Entities;

public class LeadScoringRule : BaseEntity
{
    public string Name { get; set; } = default!;

    public int MaxPoints { get; set; }

    public decimal WeightPercent { get; set; }

    public bool IsActive { get; set; }

    public int SortOrder { get; set; }
}
