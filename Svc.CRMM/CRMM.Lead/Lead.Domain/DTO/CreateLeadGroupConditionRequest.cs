namespace Lead.Domain.DTO;

public sealed record CreateLeadGroupConditionRequest(
    int SortOrder,
    string Field,
    string Operator,
    string Value);
