using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Queries;

public sealed class LeadQueries
{
    private readonly ILeadDbContext _db;

    public LeadQueries(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LeadDto>> ListAsync(CancellationToken cancellationToken)
    {
        var items = await _db.Leads
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return items.Select(ToDto).ToList();
    }

    public async Task<IReadOnlyList<AssignedLeadRowDto>> ListAssignedAsync(CancellationToken cancellationToken)
    {
        var leads = await _db.Leads
            .AsNoTracking()
            .Where(x => x.AssignedToUserId != null)
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);

        var sourceIds = leads.Select(x => x.SourceId).Distinct().ToList();
        var statusIds = leads.Select(x => x.StatusId).Distinct().ToList();
        var leadIds = leads.Select(x => x.Id).ToList();

        var sources = await _db.LeadSources
            .AsNoTracking()
            .Where(x => sourceIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);

        var statuses = await _db.LeadStatuses
            .AsNoTracking()
            .Where(x => statusIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);

        var latestScores = await _db.LeadScoreHistories
            .AsNoTracking()
            .Where(x => leadIds.Contains(x.LeadId))
            .GroupBy(x => x.LeadId)
            .Select(g => new
            {
                LeadId = g.Key,
                Score = g.OrderByDescending(x => x.ScoredAt).Select(x => (int?)x.Score).FirstOrDefault()
            })
            .ToDictionaryAsync(x => x.LeadId, x => x.Score, cancellationToken);

        return leads.Select(l => new AssignedLeadRowDto(
            Id: l.Id,
            LeadNo: l.LeadNo,
            FirstName: l.FirstName,
            LastName: l.LastName,
            JobTitle: l.JobTitle,
            CompanyName: l.CompanyName,
            Email: l.Email,
            Phone: l.Phone,
            SourceId: l.SourceId,
            SourceName: sources.TryGetValue(l.SourceId, out var sourceName) ? sourceName : string.Empty,
            StatusId: l.StatusId,
            StatusName: statuses.TryGetValue(l.StatusId, out var statusName) ? statusName : string.Empty,
            LatestScore: latestScores.TryGetValue(l.Id, out var score) ? score : null,
            AssignedToUserId: l.AssignedToUserId,
            AssignedToName: l.AssignedToName,
            UpdatedAt: l.UpdatedAt)).ToList();
    }

    public async Task<LeadDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return lead is null ? null : ToDto(lead);
    }

    public async Task<LeadDetailDto?> GetDetailAsync(Guid id, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (lead is null) return null;

        var activities = await _db.LeadActivities
            .AsNoTracking()
            .Where(x => x.LeadId == id)
            .OrderByDescending(x => x.ActivityAt)
            .ToListAsync(cancellationToken);

        var scoreHistory = await _db.LeadScoreHistories
            .AsNoTracking()
            .Where(x => x.LeadId == id)
            .OrderByDescending(x => x.ScoredAt)
            .ToListAsync(cancellationToken);

        var conversionHistory = await _db.LeadConversionLogs
            .AsNoTracking()
            .Where(x => x.LeadId == id)
            .OrderByDescending(x => x.ConvertedAt)
            .ToListAsync(cancellationToken);

        var latestScore = scoreHistory.FirstOrDefault()?.Score;

        return new LeadDetailDto(
            Lead: ToDto(lead),
            LatestScore: latestScore,
            Activities: activities.Select(x => new LeadActivityDto(x.Id, x.LeadId, x.ActivityType, x.Notes, x.ActivityAt)).ToList(),
            ScoreHistory: scoreHistory.Select(x => new LeadScoreHistoryDto(x.Id, x.LeadId, x.Score, x.Reason, x.ScoredAt)).ToList(),
            ConversionHistory: conversionHistory.Select(x => new LeadConversionLogDto(x.Id, x.LeadId, x.ConvertedTo, x.TargetId, x.ConvertedBy, x.ConvertedAt)).ToList());
    }

    private static LeadDto ToDto(Lead.Domain.Entities.Lead lead) => new(
        lead.Id,
        lead.LeadNo,
        lead.FirstName,
        lead.LastName,
        lead.CompanyName,
        lead.CompanySize,
        lead.JobTitle,
        lead.Industry,
        lead.Budget,
        lead.Timeline,
        lead.Email,
        lead.Phone,
        lead.SourceId,
        lead.StatusId,
        lead.StageId,
        lead.AssignedToUserId,
        lead.AssignedToName,
        lead.CreatedAt,
        lead.UpdatedAt);
}
