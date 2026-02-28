namespace Lead.Domain.DTO;

public sealed record CreateLeadSourceRequest(
    string Name,
    int SortOrder,
    bool IsActive);
