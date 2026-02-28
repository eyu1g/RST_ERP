namespace Lead.Domain.DTO;

public sealed record LeadIndustryAdminDto(
    Guid Id,
    string Name,
    int SortOrder,
    bool IsActive,
    DateTime CreatedAt);
