using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class UpdateLeadGroupConditionCommand
{
    private readonly ILeadDbContext _db;

    public UpdateLeadGroupConditionCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadGroupConditionDto?> ExecuteAsync(Guid id, UpdateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadGroupConditions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;

        var now = DateTime.UtcNow;
        entity.SortOrder = request.SortOrder;
        entity.Field = request.Field;
        entity.Operator = request.Operator;
        entity.Value = request.Value;
        entity.DateMod = now;

        await _db.SaveChangesAsync(cancellationToken);

        return new LeadGroupConditionDto(entity.Id, entity.LeadGroupId, entity.SortOrder, entity.Field, entity.Operator, entity.Value, entity.DateAdd, entity.DateMod ?? entity.DateAdd);
    }
}
