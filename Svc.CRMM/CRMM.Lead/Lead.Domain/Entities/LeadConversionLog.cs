namespace Lead.Domain.Entities;

public sealed class LeadConversionLog
{
    public Guid Id { get; private set; }
    public Guid LeadId { get; private set; }
    public string ConvertedTo { get; private set; } = string.Empty;
    public Guid? TargetId { get; private set; }
    public string? ConvertedBy { get; private set; }
    public DateTimeOffset ConvertedAt { get; private set; }

    private LeadConversionLog() { }

    public LeadConversionLog(Guid id, Guid leadId, string convertedTo, Guid? targetId, string? convertedBy, DateTimeOffset convertedAt)
    {
        Id = id;
        LeadId = leadId;
        ConvertedTo = convertedTo;
        TargetId = targetId;
        ConvertedBy = convertedBy;
        ConvertedAt = convertedAt;
    }
}
