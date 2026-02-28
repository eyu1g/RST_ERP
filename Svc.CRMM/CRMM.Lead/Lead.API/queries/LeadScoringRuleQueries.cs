using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Queries;

public sealed class LeadScoringRuleQueries
{
    private readonly ILeadDbContext _db;

    public LeadScoringRuleQueries(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LeadScoringRuleDto>> ListAsync(CancellationToken cancellationToken)
    {
        return await _db.LeadScoringRules
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => new LeadScoringRuleDto(x.Id, x.Name, x.MaxPoints, x.WeightPercent, x.IsActive, x.DateAdd))
            .ToListAsync(cancellationToken);
    }

    public async Task<LeadScoringRuleDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _db.LeadScoringRules
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return item is null
            ? null
            : new LeadScoringRuleDto(item.Id, item.Name, item.MaxPoints, item.WeightPercent, item.IsActive, item.DateAdd);
    }
}
