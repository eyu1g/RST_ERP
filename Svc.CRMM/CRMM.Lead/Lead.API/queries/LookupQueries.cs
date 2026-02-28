using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Queries;

public sealed class LookupQueries
{
    private readonly ILeadDbContext _db;

    public LookupQueries(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LookupItemDto>> ListStatusesAsync(CancellationToken cancellationToken)
    {
        return await _db.LeadStatuses
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => new LookupItemDto(x.Id, x.Name, x.SortOrder))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LookupItemDto>> ListSourcesAsync(CancellationToken cancellationToken)
    {
        return await _db.LeadSources
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LookupItemDto(x.Id, x.Name, 0))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LookupItemDto>> ListStagesAsync(CancellationToken cancellationToken)
    {
        return await _db.LeadStages
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => new LookupItemDto(x.Id, x.Name, x.SortOrder))
            .ToListAsync(cancellationToken);
    }
}
