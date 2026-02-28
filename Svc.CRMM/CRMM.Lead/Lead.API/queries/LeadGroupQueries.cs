using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Queries;

public sealed class LeadGroupQueries
{
    private readonly ILeadDbContext _db;

    public LeadGroupQueries(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LeadGroupDto>> ListAsync(CancellationToken cancellationToken)
    {
        var groups = await _db.LeadGroups
            .AsNoTracking()
            .OrderBy(x => x.Code)
            .ToListAsync(cancellationToken);

        var groupIds = groups.Select(x => x.Id).ToList();

        var conditions = await _db.LeadGroupConditions
            .AsNoTracking()
            .Where(x => groupIds.Contains(x.LeadGroupId))
            .OrderBy(x => x.SortOrder)
            .ToListAsync(cancellationToken);

        var leadCounts = await ComputeLeadCountsAsync(groups, conditions, cancellationToken);

        return groups
            .Select(g => new LeadGroupDto(
                g.Id,
                g.Code,
                g.Name,
                g.IsActive,
                leadCounts.TryGetValue(g.Id, out var count) ? count : 0,
                g.DateAdd,
                g.DateMod ?? g.DateAdd))
            .ToList();
    }

    public async Task<(LeadGroupDto Group, IReadOnlyList<LeadGroupConditionDto> Conditions)?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var group = await _db.LeadGroups
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (group is null) return null;

        var conditions = await _db.LeadGroupConditions
            .AsNoTracking()
            .Where(x => x.LeadGroupId == id)
            .OrderBy(x => x.SortOrder)
            .ToListAsync(cancellationToken);

        var leadCounts = await ComputeLeadCountsAsync(new[] { group }, conditions, cancellationToken);
        var leadCount = leadCounts.TryGetValue(group.Id, out var count) ? count : 0;

        var groupDto = new LeadGroupDto(group.Id, group.Code, group.Name, group.IsActive, leadCount, group.DateAdd, group.DateMod ?? group.DateAdd);
        var conditionDtos = conditions.Select(ToDto).ToList();

        return (groupDto, conditionDtos);
    }

    public async Task<IReadOnlyList<LeadGroupConditionDto>> ListConditionsAsync(Guid leadGroupId, CancellationToken cancellationToken)
    {
        var items = await _db.LeadGroupConditions
            .AsNoTracking()
            .Where(x => x.LeadGroupId == leadGroupId)
            .OrderBy(x => x.SortOrder)
            .ToListAsync(cancellationToken);

        return items.Select(ToDto).ToList();
    }

    private async Task<Dictionary<Guid, int>> ComputeLeadCountsAsync(
        IEnumerable<Lead.Domain.Entities.LeadGroup> groups,
        IReadOnlyList<Lead.Domain.Entities.LeadGroupCondition> allConditions,
        CancellationToken cancellationToken)
    {
        var groupList = groups.ToList();
        var groupIds = groupList.Select(x => x.Id).ToHashSet();

        if (groupList.Count == 0) return new Dictionary<Guid, int>();

        var conditionsByGroup = allConditions
            .Where(c => groupIds.Contains(c.LeadGroupId))
            .GroupBy(c => c.LeadGroupId)
            .ToDictionary(g => g.Key, g => g.OrderBy(x => x.SortOrder).ToList());

        var leads = await _db.Leads.AsNoTracking().ToListAsync(cancellationToken);

        var leadIds = leads.Select(x => x.Id).ToList();
        var latestScores = await _db.LeadScoreHistories
            .AsNoTracking()
            .Where(x => leadIds.Contains(x.LeadId))
            .GroupBy(x => x.LeadId)
            .Select(g => new { LeadId = g.Key, Score = g.OrderByDescending(x => x.ScoredAt).Select(x => x.Score).FirstOrDefault() })
            .ToDictionaryAsync(x => x.LeadId, x => (int?)x.Score, cancellationToken);

        var result = new Dictionary<Guid, int>();

        foreach (var group in groupList)
        {
            if (!conditionsByGroup.TryGetValue(group.Id, out var conditions) || conditions.Count == 0)
            {
                result[group.Id] = 0;
                continue;
            }

            var count = 0;
            foreach (var lead in leads)
            {
                var score = latestScores.TryGetValue(lead.Id, out var s) ? s : null;
                if (MatchesAll(lead, score, conditions)) count++;
            }

            result[group.Id] = count;
        }

        return result;
    }

    private static bool MatchesAll(Lead.Domain.Entities.Lead lead, int? score, IReadOnlyList<Lead.Domain.Entities.LeadGroupCondition> conditions)
    {
        foreach (var condition in conditions)
        {
            if (!Matches(lead, score, condition)) return false;
        }
        return true;
    }

    private static bool Matches(Lead.Domain.Entities.Lead lead, int? score, Lead.Domain.Entities.LeadGroupCondition condition)
    {
        var field = condition.Field?.Trim();
        var op = condition.Operator?.Trim();
        var value = condition.Value?.Trim();

        if (string.IsNullOrWhiteSpace(field) || string.IsNullOrWhiteSpace(op)) return false;
        value ??= string.Empty;

        var normalizedField = new string(field.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();

        return normalizedField switch
        {
            "industry" => CompareString(lead.Industry, op, value),
            "companysize" => CompareString(lead.CompanySize, op, value),
            "statusid" => CompareGuid(lead.StatusId, op, value),
            "stageid" => CompareGuid(lead.StageId, op, value),
            "sourceid" => CompareGuid(lead.SourceId, op, value),
            "budget" => CompareDecimal(lead.Budget, op, value),
            "score" => CompareInt(score, op, value),
            _ => false
        };
    }

    private static bool CompareString(string? actual, string op, string expected)
    {
        actual ??= string.Empty;

        return op.ToLowerInvariant() switch
        {
            "equals" => string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase),
            "notequals" => !string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase),
            "contains" => actual.Contains(expected, StringComparison.OrdinalIgnoreCase),
            _ => false
        };
    }

    private static bool CompareGuid(Guid actual, string op, string expected)
    {
        if (!Guid.TryParse(expected, out var guid)) return false;

        return op.ToLowerInvariant() switch
        {
            "equals" => actual == guid,
            "notequals" => actual != guid,
            _ => false
        };
    }

    private static bool CompareDecimal(decimal? actual, string op, string expected)
    {
        if (!decimal.TryParse(expected, out var target) || actual is null) return false;

        return op.ToLowerInvariant() switch
        {
            "equals" => actual.Value == target,
            "notequals" => actual.Value != target,
            "greaterthan" => actual.Value > target,
            "lessthan" => actual.Value < target,
            _ => false
        };
    }

    private static bool CompareInt(int? actual, string op, string expected)
    {
        if (!int.TryParse(expected, out var target) || actual is null) return false;

        return op.ToLowerInvariant() switch
        {
            "equals" => actual.Value == target,
            "notequals" => actual.Value != target,
            "greaterthan" => actual.Value > target,
            "lessthan" => actual.Value < target,
            _ => false
        };
    }

    private static LeadGroupConditionDto ToDto(Lead.Domain.Entities.LeadGroupCondition condition) => new(
        condition.Id,
        condition.LeadGroupId,
        condition.SortOrder,
        condition.Field,
        condition.Operator,
        condition.Value,
        condition.DateAdd,
        condition.DateMod ?? condition.DateAdd);
}
