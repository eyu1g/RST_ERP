using Lead.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class DeleteLeadSourceAdminCommand
{
    private readonly ILeadDbContext _db;

    public DeleteLeadSourceAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadSources.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;

        _db.LeadSources.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
