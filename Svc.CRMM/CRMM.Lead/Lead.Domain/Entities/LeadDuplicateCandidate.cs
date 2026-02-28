namespace Lead.Domain.Entities;

public class LeadDuplicateCandidate : BaseEntity
{
    public Guid LeadId { get;set; }
    public Guid CandidateLeadId { get;set; }
    public decimal MatchScore { get;set; }
    public DateTime DetectedAt { get;set; }
}
