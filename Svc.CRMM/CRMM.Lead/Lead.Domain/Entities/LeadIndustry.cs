namespace Lead.Domain.Entities;

public class LeadIndustry : BaseEntity
{
    public string Name { get; set; } = default!;

    public bool IsActive { get; set; }

    public int SortOrder { get; set; }
}
