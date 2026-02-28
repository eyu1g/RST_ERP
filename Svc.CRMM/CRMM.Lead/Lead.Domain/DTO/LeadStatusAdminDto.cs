namespace Lead.Domain.DTO;

public sealed record LeadStatusAdminDto(
    Guid Id,
    string Name,
    int Priority,
    bool IsActive,
    DateTime CreatedAt);
