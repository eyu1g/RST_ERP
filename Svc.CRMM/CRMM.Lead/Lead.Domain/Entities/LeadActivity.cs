namespace Lead.Domain.Entities;

public sealed class LeadActivity
{
    public Guid Id { get; private set; }
    public Guid LeadId { get; private set; }
    public string ActivityType { get; private set; } = string.Empty;
    public string? Notes { get; private set; }
    public DateTimeOffset ActivityAt { get; private set; }

    private LeadActivity() { }

    public LeadActivity(Guid id, Guid leadId, string activityType, string? notes, DateTimeOffset activityAt)
    {
        Id = id;
        LeadId = leadId;
        ActivityType = activityType;
        Notes = notes;
        ActivityAt = activityAt;
    }
}
