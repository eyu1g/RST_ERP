namespace Lead.Domain.DTO;

public sealed record LeadGroupSettingsDto(
    IReadOnlyList<LeadGroupConditionFieldDto> Fields,
    IReadOnlyList<LeadGroupConditionOperatorDto> Operators);

public sealed record LeadGroupConditionFieldDto(
    string Key,
    string Label,
    string ValueType,
    IReadOnlyList<string> AllowedOperators);

public sealed record LeadGroupConditionOperatorDto(
    string Key,
    string Label);
