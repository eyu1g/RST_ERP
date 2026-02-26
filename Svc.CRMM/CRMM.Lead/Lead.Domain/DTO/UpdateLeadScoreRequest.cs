namespace Lead.Domain.DTO;

public sealed record UpdateLeadScoreRequest(
    int Score,
    string? Reason);
