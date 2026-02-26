namespace Lead.Domain.DTO;

public sealed record ConvertLeadResultDto(
    Guid LeadId,
    Guid ConversionLogId,
    Guid StatusId,
    Guid StageId,
    DateTimeOffset ConvertedAt);
