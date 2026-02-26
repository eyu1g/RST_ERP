using Lead.Domain.DTO;
using LeadEntity = Lead.Domain.Entities.Lead;
using Lead.App.Helpers;
using Lead.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lead.App.Service;

public sealed class LeadService
{
    private readonly ILeadDbContext _db;

    private static readonly Guid ConvertedStatusId = Guid.Parse("11111111-1111-1111-1111-111111111116");
    private static readonly Guid ConversionStageId = Guid.Parse("22222222-2222-2222-2222-222222222226");

    public LeadService(ILeadDbContext db)
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

        if (lead is null)
        {
            return null;
        }

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

    public async Task<LeadDto> CreateAsync(CreateLeadRequest request, CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.UtcNow;

        var sourceId = request.SourceId ?? await _db.LeadSources
            .AsNoTracking()
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        var statusId = request.StatusId ?? await _db.LeadStatuses
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        var stageId = request.StageId ?? await _db.LeadStages
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (statusId == Guid.Empty)
        {
            throw new InvalidOperationException("No LeadStatus rows exist. Seed LeadStatus before creating a Lead.");
        }

        if (stageId == Guid.Empty)
        {
            throw new InvalidOperationException("No LeadStage rows exist. Seed LeadStage before creating a Lead.");
        }

        if (sourceId == Guid.Empty)
        {
            throw new InvalidOperationException("No LeadSource rows exist. Seed LeadSource before creating a Lead.");
        }

        var lead = new LeadEntity(
            id: Guid.NewGuid(),
            leadNo: LeadNoGenerator.NewLeadNo(now),
            firstName: request.FirstName,
            lastName: request.LastName,
            companyName: request.CompanyName,
            companySize: request.CompanySize,
            jobTitle: request.JobTitle,
            industry: request.Industry,
            budget: request.Budget,
            timeline: request.Timeline,
            email: request.Email,
            phone: request.Phone,
            sourceId: sourceId,
            statusId: statusId,
            stageId: stageId,
            assignedToUserId: null,
            assignedToName: null,
            createdAt: now,
            updatedAt: now);

        _db.Leads.Add(lead);
        await _db.SaveChangesAsync(cancellationToken);
        return ToDto(lead);
    }

    public async Task<LeadDto?> AssignAsync(Guid id, AssignLeadRequest request, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null)
        {
            return null;
        }

        var now = DateTimeOffset.UtcNow;
        lead.SetAssignment(request.AssignedToUserId, request.AssignedToName, now);
        await _db.SaveChangesAsync(cancellationToken);
        return ToDto(lead);
    }

    public async Task<LeadDto?> ChangeStatusAsync(Guid id, ChangeLeadStatusRequest request, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null)
        {
            return null;
        }

        var now = DateTimeOffset.UtcNow;
        lead.UpdateBasics(
            firstName: lead.FirstName,
            lastName: lead.LastName,
            companyName: lead.CompanyName,
            companySize: lead.CompanySize,
            jobTitle: lead.JobTitle,
            industry: lead.Industry,
            budget: lead.Budget,
            timeline: lead.Timeline,
            email: lead.Email,
            phone: lead.Phone,
            sourceId: lead.SourceId,
            statusId: request.StatusId,
            stageId: lead.StageId,
            updatedAt: now);

        await _db.SaveChangesAsync(cancellationToken);
        return ToDto(lead);
    }

    public async Task<LeadScoreHistoryDto?> AddScoreAsync(Guid id, UpdateLeadScoreRequest request, CancellationToken cancellationToken)
    {
        var leadExists = await _db.Leads
            .AsNoTracking()
            .AnyAsync(x => x.Id == id, cancellationToken);

        if (!leadExists)
        {
            return null;
        }

        var now = DateTimeOffset.UtcNow;
        var entity = new Lead.Domain.Entities.LeadScoreHistory(
            id: Guid.NewGuid(),
            leadId: id,
            score: request.Score,
            reason: request.Reason,
            scoredAt: now);

        _db.LeadScoreHistories.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadScoreHistoryDto(entity.Id, entity.LeadId, entity.Score, entity.Reason, entity.ScoredAt);
    }

    public async Task<ConvertLeadResultDto?> ConvertAsync(Guid id, ConvertLeadRequest request, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null)
        {
            return null;
        }

        var now = DateTimeOffset.UtcNow;

        var convertedToParts = new List<string>();
        if (request.CreateContact) convertedToParts.Add("Contact");
        if (request.CreateAccount) convertedToParts.Add("Account");
        if (request.CreateOpportunity) convertedToParts.Add("Opportunity");
        var convertedTo = convertedToParts.Count == 0 ? "Converted" : string.Join(",", convertedToParts);

        var log = new Lead.Domain.Entities.LeadConversionLog(
            id: Guid.NewGuid(),
            leadId: id,
            convertedTo: convertedTo,
            targetId: null,
            convertedBy: request.ConvertedBy,
            convertedAt: now);

        _db.LeadConversionLogs.Add(log);

        lead.UpdateBasics(
            firstName: lead.FirstName,
            lastName: lead.LastName,
            companyName: lead.CompanyName,
            companySize: lead.CompanySize,
            jobTitle: lead.JobTitle,
            industry: lead.Industry,
            budget: lead.Budget,
            timeline: lead.Timeline,
            email: lead.Email,
            phone: lead.Phone,
            sourceId: lead.SourceId,
            statusId: ConvertedStatusId,
            stageId: ConversionStageId,
            updatedAt: now);

        await _db.SaveChangesAsync(cancellationToken);

        return new ConvertLeadResultDto(
            LeadId: id,
            ConversionLogId: log.Id,
            StatusId: lead.StatusId,
            StageId: lead.StageId,
            ConvertedAt: log.ConvertedAt);
    }

    public async Task<LeadDto?> UpdateAsync(Guid id, UpdateLeadRequest request, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null)
        {
            return null;
        }

        var now = DateTimeOffset.UtcNow;
        lead.UpdateBasics(
            firstName: request.FirstName,
            lastName: request.LastName,
            companyName: request.CompanyName,
            companySize: request.CompanySize,
            jobTitle: request.JobTitle,
            industry: request.Industry,
            budget: request.Budget,
            timeline: request.Timeline,
            email: request.Email,
            phone: request.Phone,
            sourceId: request.SourceId,
            statusId: request.StatusId,
            stageId: request.StageId,
            updatedAt: now);

        await _db.SaveChangesAsync(cancellationToken);
        return ToDto(lead);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null)
        {
            return false;
        }

        _db.Leads.Remove(lead);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static LeadDto ToDto(LeadEntity lead) => new(
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
