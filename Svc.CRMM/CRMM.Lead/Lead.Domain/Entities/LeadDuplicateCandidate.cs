namespace Lead.Domain.Entities;

public sealed class LeadDuplicateCandidate
{
    public Guid Id { get; private set; }
    public Guid LeadId { get; private set; }
    public Guid CandidateLeadId { get; private set; }
    public decimal MatchScore { get; private set; }
    public DateTimeOffset DetectedAt { get; private set; }

    private LeadDuplicateCandidate() { }

    public LeadDuplicateCandidate(Guid id, Guid leadId, Guid candidateLeadId, decimal matchScore, DateTimeOffset detectedAt)
    {
        Id = id;
        LeadId = leadId;
        CandidateLeadId = candidateLeadId;
        MatchScore = matchScore;
        DetectedAt = detectedAt;
    }
}
