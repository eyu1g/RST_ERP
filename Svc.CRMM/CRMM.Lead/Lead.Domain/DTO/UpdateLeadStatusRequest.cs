namespace Lead.Domain.DTO;

public sealed record UpdateLeadStatusRequest(
    string Name,
    int Priority);
