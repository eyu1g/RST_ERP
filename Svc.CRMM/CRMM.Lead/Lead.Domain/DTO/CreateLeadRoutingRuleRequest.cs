namespace Lead.Domain.DTO;

public sealed record CreateLeadRoutingRuleRequest(
    int Priority,
    string Name,
    Guid? AssignToUserId,
    string? AssignToName,
    bool IsActive);
