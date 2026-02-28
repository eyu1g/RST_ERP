namespace Lead.Domain.DTO;

public sealed record LeadConversionLogDto(
    Guid Id,
    Guid LeadId,
    string ConvertedTo,
    Guid? TargetId,
    string? ConvertedBy,
    DateTime ConvertedAt);
