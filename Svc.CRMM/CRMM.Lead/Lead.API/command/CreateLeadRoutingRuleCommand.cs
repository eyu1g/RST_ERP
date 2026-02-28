using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Lead.Domain.Entities;

namespace Lead.Api.Command;

public sealed class CreateLeadRoutingRuleCommand
{
    private readonly ILeadDbContext _db;

    public CreateLeadRoutingRuleCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadRoutingRuleDto> ExecuteAsync(CreateLeadRoutingRuleRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var entity = new LeadRoutingRule
        {
            Id = Guid.NewGuid(),
            Priority = request.Priority,
            Name = request.Name,
            AssignToUserId = request.AssignToUserId,
            AssignToName = request.AssignToName,
            IsActive = request.IsActive,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadRoutingRules.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadRoutingRuleDto(entity.Id, entity.Priority, entity.Name, entity.AssignToUserId, entity.AssignToName, entity.IsActive, entity.DateAdd, 0);
    }
}
