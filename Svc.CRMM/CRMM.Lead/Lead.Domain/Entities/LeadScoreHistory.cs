namespace Lead.Domain.Entities;

public sealed class LeadScoreHistory
{
    public Guid Id { get; private set; }
    public Guid LeadId { get; private set; }
    public int Score { get; private set; }
    public string? Reason { get; private set; }
    public DateTimeOffset ScoredAt { get; private set; }

    private LeadScoreHistory() { }

    public LeadScoreHistory(Guid id, Guid leadId, int score, string? reason, DateTimeOffset scoredAt)
    {
        Id = id;
        LeadId = leadId;
        Score = score;
        Reason = reason;
        ScoredAt = scoredAt;
    }
}
