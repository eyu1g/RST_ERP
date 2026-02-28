namespace Lead.Domain.Entities;

public class LeadScoreHistory : BaseEntity
{
    public Guid LeadId { get; set; }
    public int Score { get; set; }
    public string? Reason { get; set; }
    public DateTime ScoredAt { get; set; }
}
