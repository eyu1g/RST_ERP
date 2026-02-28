namespace Lead.Domain.DTO;

public sealed record UpdateLeadScoringRuleRequest(
    string Name,
    int MaxPoints,
    decimal WeightPercent,
    int SortOrder);
