namespace Lead.Domain.Entities;

public class LeadMergeLog : BaseEntity
{
    public Guid PrimaryLeadId { get;set; }
    public Guid MergedLeadId { get;set; }
    public string? MergedBy { get;set; }
    public DateTime MergedAt { get;set; }
}
