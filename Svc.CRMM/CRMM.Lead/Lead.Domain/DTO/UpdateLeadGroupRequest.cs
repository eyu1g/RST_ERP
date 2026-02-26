namespace Lead.Domain.DTO;

public sealed record UpdateLeadGroupRequest(
    string Code,
    string Name,
    bool IsActive);
