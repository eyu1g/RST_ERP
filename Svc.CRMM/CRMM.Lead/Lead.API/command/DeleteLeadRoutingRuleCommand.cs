using Lead.App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class DeleteLeadRoutingRuleCommand
{
    private readonly ILeadDbContext _db;

    public DeleteLeadRoutingRuleCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadRoutingRules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;

        var conditions = await _db.LeadRoutingRuleConditions
            .Where(x => x.LeadRoutingRuleId == id)
            .ToListAsync(cancellationToken);

        _db.LeadRoutingRuleConditions.RemoveRange(conditions);
        _db.LeadRoutingRules.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
