namespace Lead.Domain.DTO;

public sealed record UpdateLeadGroupConditionRequest(
    int SortOrder,
    string Field,
    string Operator,
    string Value);
