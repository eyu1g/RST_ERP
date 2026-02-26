namespace Lead.Domain.DTO;

public sealed record LeadDetailDto(
    LeadDto Lead,
    int? LatestScore,
    IReadOnlyList<LeadActivityDto> Activities,
    IReadOnlyList<LeadScoreHistoryDto> ScoreHistory,
    IReadOnlyList<LeadConversionLogDto> ConversionHistory);
