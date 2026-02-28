namespace Lead.Domain.DTO;

public sealed record LeadDto(
    Guid Id,
    string LeadNo,
    string? FirstName,
    string? LastName,
    string? CompanyName,
    string? CompanySize,
    string? JobTitle,
    string? Industry,
    decimal? Budget,
    string? Timeline,
    string? Email,
    string? Phone,
    Guid SourceId,
    Guid StatusId,
    Guid StageId,
    Guid? AssignedToUserId,
    string? AssignedToName,
    DateTime CreatedAt,
    DateTime UpdatedAt);
