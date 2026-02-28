namespace Lead.Domain.DTO;

public sealed record LeadRoutingRuleDto(
    Guid Id,
    int Priority,
    string Name,
    Guid? AssignToUserId,
    string? AssignToName,
    bool IsActive,
    DateTime CreatedAt,
    int ConditionsCount);
