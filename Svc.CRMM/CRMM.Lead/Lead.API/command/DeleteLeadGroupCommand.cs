using Lead.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class DeleteLeadGroupCommand
{
    private readonly ILeadDbContext _db;

    public DeleteLeadGroupCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadGroups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;

        var conditions = await _db.LeadGroupConditions
            .Where(x => x.LeadGroupId == id)
            .ToListAsync(cancellationToken);

        _db.LeadGroupConditions.RemoveRange(conditions);
        _db.LeadGroups.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
