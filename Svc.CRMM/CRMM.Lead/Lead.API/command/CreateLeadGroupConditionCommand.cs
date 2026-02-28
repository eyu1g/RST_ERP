using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class CreateLeadGroupConditionCommand
{
    private readonly ILeadDbContext _db;

    public CreateLeadGroupConditionCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadGroupConditionDto> ExecuteAsync(Guid leadGroupId, CreateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var groupExists = await _db.LeadGroups.AnyAsync(x => x.Id == leadGroupId, cancellationToken);
        if (!groupExists)
            throw new InvalidOperationException("LeadGroup not found.");

        var now = DateTime.UtcNow;
        var entity = new Lead.Domain.Entities.LeadGroupCondition
        {
            Id = Guid.NewGuid(),
            LeadGroupId = leadGroupId,
            SortOrder = request.SortOrder,
            Field = request.Field,
            Operator = request.Operator,
            Value = request.Value,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadGroupConditions.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadGroupConditionDto(entity.Id, entity.LeadGroupId, entity.SortOrder, entity.Field, entity.Operator, entity.Value, entity.DateAdd, entity.DateMod ?? entity.DateAdd);
    }
}
