using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Queries;

public sealed class LeadSourceAdminQueries
{
    private readonly ILeadDbContext _db;

    public LeadSourceAdminQueries(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LeadSourceAdminDto>> ListAsync(CancellationToken cancellationToken)
    {
        return await _db.LeadSources
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => new LeadSourceAdminDto(x.Id, x.Name, x.SortOrder, x.IsActive, x.DateAdd))
            .ToListAsync(cancellationToken);
    }

    public async Task<LeadSourceAdminDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _db.LeadSources
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return item is null
            ? null
            : new LeadSourceAdminDto(item.Id, item.Name, item.SortOrder, item.IsActive, item.DateAdd);
    }
}
