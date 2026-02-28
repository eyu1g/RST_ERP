namespace Lead.Domain.DTO;

public sealed record LeadSourceAdminDto(
    Guid Id,
    string Name,
    int SortOrder,
    bool IsActive,
    DateTime CreatedAt);
