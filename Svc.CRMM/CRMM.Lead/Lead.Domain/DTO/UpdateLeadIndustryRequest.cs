namespace Lead.Domain.DTO;

public sealed record UpdateLeadIndustryRequest(
    string Name,
    int SortOrder);
