namespace Lead.Domain.DTO;

public sealed record LeadGroupDto(
    Guid Id,
    string Code,
    string Name,
    bool IsActive,
    int LeadCount,
    DateTime CreatedAt,
    DateTime UpdatedAt);
