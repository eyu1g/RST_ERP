namespace Lead.Domain.DTO;

public sealed record CreateLeadScoringRuleRequest(
    string Name,
    int MaxPoints,
    decimal WeightPercent,
    bool IsActive,
    int SortOrder);
