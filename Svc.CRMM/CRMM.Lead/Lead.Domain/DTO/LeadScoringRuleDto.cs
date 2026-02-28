namespace Lead.Domain.DTO;

public sealed record LeadScoringRuleDto(
    Guid Id,
    string Name,
    int MaxPoints,
    decimal WeightPercent,
    bool IsActive,
    DateTime CreatedAt);
