using Lead.App.Interfaces;
using Lead.Domain.DTO;
using LeadGroupEntity = Lead.Domain.Entities.LeadGroup;
using LeadGroupConditionEntity = Lead.Domain.Entities.LeadGroupCondition;
using LeadEntity = Lead.Domain.Entities.Lead;
using Microsoft.EntityFrameworkCore;

namespace Lead.App.Service;

public sealed class LeadGroupService
{
    private readonly ILeadDbContext _db;

    public LeadGroupService(ILeadDbContext db)
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

        if (group is null)
        {
            return null;
        }

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

    public async Task<LeadGroupDto> CreateAsync(CreateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var entity = new LeadGroupEntity
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            IsActive = request.IsActive,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadGroups.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadGroupDto(entity.Id, entity.Code, entity.Name, entity.IsActive, 0, entity.DateAdd, entity.DateMod ?? entity.DateAdd);
    }

    public async Task<LeadGroupDto?> UpdateAsync(Guid id, UpdateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadGroups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var now = DateTime.UtcNow;
        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.IsActive = request.IsActive;
        entity.DateMod = now;

        await _db.SaveChangesAsync(cancellationToken);

        var conditions = await _db.LeadGroupConditions
            .AsNoTracking()
            .Where(x => x.LeadGroupId == id)
            .ToListAsync(cancellationToken);

        var leadCounts = await ComputeLeadCountsAsync(new[] { entity }, conditions, cancellationToken);
        var leadCount = leadCounts.TryGetValue(entity.Id, out var count) ? count : 0;

        return new LeadGroupDto(entity.Id, entity.Code, entity.Name, entity.IsActive, leadCount, entity.DateAdd, entity.DateMod ?? entity.DateAdd);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadGroups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        var conditions = await _db.LeadGroupConditions
            .Where(x => x.LeadGroupId == id)
            .ToListAsync(cancellationToken);

        _db.LeadGroupConditions.RemoveRange(conditions);
        _db.LeadGroups.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
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

    public async Task<LeadGroupConditionDto> CreateConditionAsync(Guid leadGroupId, CreateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var groupExists = await _db.LeadGroups.AnyAsync(x => x.Id == leadGroupId, cancellationToken);
        if (!groupExists)
        {
            throw new InvalidOperationException("LeadGroup not found.");
        }

        var now = DateTime.UtcNow;
        var entity = new LeadGroupConditionEntity
        {
            Id = Guid.NewGuid(),
            LeadGroupId = leadGroupId,
            SortOrder = request.SortOrder,
            Field = request.Field,
            Operator = request.Operator,
            Value = request.Value,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadGroupConditions.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return ToDto(entity);
    }

    public async Task<LeadGroupConditionDto?> UpdateConditionAsync(Guid id, UpdateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadGroupConditions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var now = DateTime.UtcNow;
        entity.SortOrder = request.SortOrder;
        entity.Field = request.Field;
        entity.Operator = request.Operator;
        entity.Value = request.Value;
        entity.DateMod = now;
        await _db.SaveChangesAsync(cancellationToken);

        return ToDto(entity);
    }

    public async Task<bool> DeleteConditionAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadGroupConditions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _db.LeadGroupConditions.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<Dictionary<Guid, int>> ComputeLeadCountsAsync(
        IEnumerable<LeadGroupEntity> groups,
        IReadOnlyList<LeadGroupConditionEntity> allConditions,
        CancellationToken cancellationToken)
    {
        var groupList = groups.ToList();
        var groupIds = groupList.Select(x => x.Id).ToHashSet();

        if (groupList.Count == 0)
        {
            return new Dictionary<Guid, int>();
        }

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
            .ToDictionaryAsync(x => x.LeadId, x => x.Score, cancellationToken);

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
                var score = latestScores.TryGetValue(lead.Id, out var s) ? s : (int?)null;
                if (MatchesAll(lead, score, conditions))
                {
                    count++;
                }
            }

            result[group.Id] = count;
        }

        return result;
    }

    private static bool MatchesAll(LeadEntity lead, int? score, IReadOnlyList<LeadGroupConditionEntity> conditions)
    {
        foreach (var condition in conditions)
        {
            if (!Matches(lead, score, condition))
            {
                return false;
            }
        }

        return true;
    }

    private static bool Matches(LeadEntity lead, int? score, LeadGroupConditionEntity condition)
    {
        var field = condition.Field?.Trim();
        var op = condition.Operator?.Trim();
        var value = condition.Value?.Trim();

        if (string.IsNullOrWhiteSpace(field) || string.IsNullOrWhiteSpace(op))
        {
            return false;
        }

        if (value is null)
        {
            value = string.Empty;
        }

        var normalizedField = new string(field.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();

        switch (normalizedField)
        {
            case "industry":
                return CompareString(lead.Industry, op, value);

            case "companysize":
                return CompareString(lead.CompanySize, op, value);

            case "statusid":
                return CompareGuid(lead.StatusId, op, value);

            case "stageid":
                return CompareGuid(lead.StageId, op, value);

            case "sourceid":
                return CompareGuid(lead.SourceId, op, value);

            case "budget":
                return CompareDecimal(lead.Budget, op, value);

            case "score":
                return CompareInt(score, op, value);

            default:
                return false;
        }
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
        if (!Guid.TryParse(expected, out var guid))
        {
            return false;
        }

        return op.ToLowerInvariant() switch
        {
            "equals" => actual == guid,
            "notequals" => actual != guid,
            _ => false
        };
    }

    private static bool CompareDecimal(decimal? actual, string op, string expected)
    {
        if (!decimal.TryParse(expected, out var target))
        {
            return false;
        }

        if (actual is null)
        {
            return false;
        }

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
        if (!int.TryParse(expected, out var target))
        {
            return false;
        }

        if (actual is null)
        {
            return false;
        }

        return op.ToLowerInvariant() switch
        {
            "equals" => actual.Value == target,
            "notequals" => actual.Value != target,
            "greaterthan" => actual.Value > target,
            "lessthan" => actual.Value < target,
            _ => false
        };
    }

    private static LeadGroupConditionDto ToDto(LeadGroupConditionEntity condition) => new(
        condition.Id,
        condition.LeadGroupId,
        condition.SortOrder,
        condition.Field,
        condition.Operator,
        condition.Value,
        condition.DateAdd,
        condition.DateMod ?? condition.DateAdd);
}
