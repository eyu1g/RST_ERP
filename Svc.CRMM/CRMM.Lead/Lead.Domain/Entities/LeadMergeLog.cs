namespace Lead.Domain.Entities;

public sealed class LeadMergeLog
{
    public Guid Id { get; private set; }
    public Guid PrimaryLeadId { get; private set; }
    public Guid MergedLeadId { get; private set; }
    public string? MergedBy { get; private set; }
    public DateTimeOffset MergedAt { get; private set; }

    private LeadMergeLog() { }

    public LeadMergeLog(Guid id, Guid primaryLeadId, Guid mergedLeadId, string? mergedBy, DateTimeOffset mergedAt)
    {
        Id = id;
        PrimaryLeadId = primaryLeadId;
        MergedLeadId = mergedLeadId;
        MergedBy = mergedBy;
        MergedAt = mergedAt;
    }
}
