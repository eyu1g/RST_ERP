using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Queries;

public sealed class LeadStatusAdminQueries
{
    private readonly ILeadDbContext _db;

    public LeadStatusAdminQueries(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LeadStatusAdminDto>> ListAsync(CancellationToken cancellationToken)
    {
        return await _db.LeadStatuses
            .AsNoTracking()
            .OrderBy(x => x.Priority)
            .Select(x => new LeadStatusAdminDto(x.Id, x.Name, x.Priority, x.IsActive, x.DateAdd))
            .ToListAsync(cancellationToken);
    }

    public async Task<LeadStatusAdminDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _db.LeadStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return item is null
            ? null
            : new LeadStatusAdminDto(item.Id, item.Name, item.Priority, item.IsActive, item.DateAdd);
    }
}
