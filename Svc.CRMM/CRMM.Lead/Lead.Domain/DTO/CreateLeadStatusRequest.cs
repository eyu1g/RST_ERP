namespace Lead.Domain.DTO;

public sealed record CreateLeadStatusRequest(
    string Name,
    int Priority,
    bool IsActive);
