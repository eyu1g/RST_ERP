namespace Lead.Domain.DTO;

public sealed record UpdateLeadSourceRequest(
    string Name,
    int SortOrder);
