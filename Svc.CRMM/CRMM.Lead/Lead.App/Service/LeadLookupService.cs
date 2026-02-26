using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.App.Service;

public sealed class LeadLookupService
{
    private readonly ILeadDbContext _db;

    public LeadLookupService(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LookupItemDto>> ListStatusesAsync(CancellationToken cancellationToken)
    {
        var items = await _db.LeadStatuses
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => new LookupItemDto(x.Id, x.Name, x.SortOrder))
            .ToListAsync(cancellationToken);

        return items;
    }

    public async Task<IReadOnlyList<LookupItemDto>> ListSourcesAsync(CancellationToken cancellationToken)
    {
        var items = await _db.LeadSources
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LookupItemDto(x.Id, x.Name, 0))
            .ToListAsync(cancellationToken);

        return items;
    }

    public async Task<IReadOnlyList<LookupItemDto>> ListStagesAsync(CancellationToken cancellationToken)
    {
        var items = await _db.LeadStages
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => new LookupItemDto(x.Id, x.Name, x.SortOrder))
            .ToListAsync(cancellationToken);

        return items;
    }
}
