namespace Lead.Domain.DTO;

public sealed record ImportLeadsResultDto(
    int TotalRows,
    int CreatedCount,
    IReadOnlyList<ImportLeadRowErrorDto> Errors);

public sealed record ImportLeadRowErrorDto(
    int RowNumber,
    string Message);
