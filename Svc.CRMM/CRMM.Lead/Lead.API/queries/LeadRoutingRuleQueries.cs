using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Queries;

public sealed class LeadRoutingRuleQueries
{
    private readonly ILeadDbContext _db;

    public LeadRoutingRuleQueries(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LeadRoutingRuleDto>> ListAsync(CancellationToken cancellationToken)
    {
        var rules = await _db.LeadRoutingRules
            .AsNoTracking()
            .OrderBy(x => x.Priority)
            .ToListAsync(cancellationToken);

        var ruleIds = rules.Select(x => x.Id).ToList();
        var conditionCounts = await _db.LeadRoutingRuleConditions
            .AsNoTracking()
            .Where(x => ruleIds.Contains(x.LeadRoutingRuleId))
            .GroupBy(x => x.LeadRoutingRuleId)
            .Select(g => new { RuleId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.RuleId, x => x.Count, cancellationToken);

        return rules.Select(x => new LeadRoutingRuleDto(
            x.Id,
            x.Priority,
            x.Name,
            x.AssignToUserId,
            x.AssignToName,
            x.IsActive,
            x.DateAdd,
            conditionCounts.TryGetValue(x.Id, out var c) ? c : 0)).ToList();
    }

    public async Task<LeadRoutingRuleDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var rule = await _db.LeadRoutingRules
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (rule is null) return null;

        var count = await _db.LeadRoutingRuleConditions
            .AsNoTracking()
            .CountAsync(x => x.LeadRoutingRuleId == id, cancellationToken);

        return new LeadRoutingRuleDto(rule.Id, rule.Priority, rule.Name, rule.AssignToUserId, rule.AssignToName, rule.IsActive, rule.DateAdd, count);
    }
}
