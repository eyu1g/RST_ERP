namespace Lead.Domain.DTO;

public sealed record CreateLeadIndustryRequest(
    string Name,
    int SortOrder,
    bool IsActive);
