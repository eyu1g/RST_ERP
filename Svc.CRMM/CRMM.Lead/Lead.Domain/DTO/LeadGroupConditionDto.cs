namespace Lead.Domain.DTO;

public sealed record LeadGroupConditionDto(
    Guid Id,
    Guid LeadGroupId,
    int SortOrder,
    string Field,
    string Operator,
    string Value,
    DateTime CreatedAt,
    DateTime UpdatedAt);
