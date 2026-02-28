using Lead.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class DeleteLeadCommand
{
    private readonly ILeadDbContext _db;

    public DeleteLeadCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null) return false;

        _db.Leads.Remove(lead);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
