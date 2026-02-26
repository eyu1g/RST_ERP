namespace Lead.Domain.DTO;

public sealed record LeadActivityDto(
    Guid Id,
    Guid LeadId,
    string ActivityType,
    string? Notes,
    DateTimeOffset ActivityAt);
