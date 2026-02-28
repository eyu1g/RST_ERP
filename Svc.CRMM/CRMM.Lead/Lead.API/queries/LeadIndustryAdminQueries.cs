using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Queries;

public sealed class LeadIndustryAdminQueries
{
    private readonly ILeadDbContext _db;

    public LeadIndustryAdminQueries(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LeadIndustryAdminDto>> ListAsync(CancellationToken cancellationToken)
    {
        return await _db.LeadIndustries
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => new LeadIndustryAdminDto(x.Id, x.Name, x.SortOrder, x.IsActive, x.DateAdd))
            .ToListAsync(cancellationToken);
    }

    public async Task<LeadIndustryAdminDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _db.LeadIndustries
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return item is null
            ? null
            : new LeadIndustryAdminDto(item.Id, item.Name, item.SortOrder, item.IsActive, item.DateAdd);
    }
}
