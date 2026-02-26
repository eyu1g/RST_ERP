namespace Lead.Domain.DTO;

public sealed record CreateLeadRequest(
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
    Guid? SourceId,
    Guid? StatusId,
    Guid? StageId);
