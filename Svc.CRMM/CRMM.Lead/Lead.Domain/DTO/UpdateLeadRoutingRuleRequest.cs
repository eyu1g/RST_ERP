namespace Lead.Domain.DTO;

public sealed record UpdateLeadRoutingRuleRequest(
    int Priority,
    string Name,
    Guid? AssignToUserId,
    string? AssignToName);
