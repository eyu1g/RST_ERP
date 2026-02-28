using Lead.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class DeleteLeadIndustryAdminCommand
{
    private readonly ILeadDbContext _db;

    public DeleteLeadIndustryAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadIndustries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;

        _db.LeadIndustries.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
