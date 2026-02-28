using Lead.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class DeleteLeadStatusAdminCommand
{
    private readonly ILeadDbContext _db;

    public DeleteLeadStatusAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadStatuses.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;

        _db.LeadStatuses.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
