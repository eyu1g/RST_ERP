namespace Lead.Domain.DTO;

public sealed record LeadScoreHistoryDto(
    Guid Id,
    Guid LeadId,
    int Score,
    string? Reason,
    DateTimeOffset ScoredAt);
