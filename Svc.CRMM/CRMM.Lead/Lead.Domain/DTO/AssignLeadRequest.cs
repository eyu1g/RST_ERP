namespace Lead.Domain.DTO;

public sealed record AssignLeadRequest(
    Guid AssignedToUserId,
    string AssignedToName);
