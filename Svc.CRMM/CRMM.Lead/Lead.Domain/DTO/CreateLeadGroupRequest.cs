namespace Lead.Domain.DTO;

public sealed record CreateLeadGroupRequest(
    string Code,
    string Name,
    bool IsActive);
