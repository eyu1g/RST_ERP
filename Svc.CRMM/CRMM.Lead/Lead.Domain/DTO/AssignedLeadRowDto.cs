namespace Lead.Domain.DTO;

public sealed record AssignedLeadRowDto(
    Guid Id,
    string LeadNo,
    string? FirstName,
    string? LastName,
    string? JobTitle,
    string? CompanyName,
    string? Email,
    string? Phone,
    Guid SourceId,
    string SourceName,
    Guid StatusId,
    string StatusName,
    int? LatestScore,
    Guid? AssignedToUserId,
    string? AssignedToName,
    DateTime UpdatedAt);
