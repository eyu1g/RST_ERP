using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class SetLeadRoutingRuleActiveCommand
{
    private readonly ILeadDbContext _db;

    public SetLeadRoutingRuleActiveCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadRoutingRuleDto?> ExecuteAsync(Guid id, bool isActive, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadRoutingRules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;

        entity.IsActive = isActive;
        entity.DateMod = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        var conditionsCount = await _db.LeadRoutingRuleConditions.CountAsync(x => x.LeadRoutingRuleId == id, cancellationToken);
        return new LeadRoutingRuleDto(entity.Id, entity.Priority, entity.Name, entity.AssignToUserId, entity.AssignToName, entity.IsActive, entity.DateAdd, conditionsCount);
    }
}
