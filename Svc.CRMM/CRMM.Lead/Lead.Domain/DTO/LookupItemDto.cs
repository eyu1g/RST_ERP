namespace Lead.Domain.DTO;

public sealed record LookupItemDto(
    Guid Id,
    string Name,
    int SortOrder);
