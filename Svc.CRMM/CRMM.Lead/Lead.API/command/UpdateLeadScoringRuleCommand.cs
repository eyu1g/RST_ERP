using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class UpdateLeadScoringRuleCommand
{
    private readonly ILeadDbContext _db;

    public UpdateLeadScoringRuleCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadScoringRuleDto?> ExecuteAsync(Guid id, UpdateLeadScoringRuleRequest request, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadScoringRules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;

        entity.Name = request.Name;
        entity.MaxPoints = request.MaxPoints;
        entity.WeightPercent = request.WeightPercent;
        entity.SortOrder = request.SortOrder;
        entity.DateMod = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        return new LeadScoringRuleDto(entity.Id, entity.Name, entity.MaxPoints, entity.WeightPercent, entity.IsActive, entity.DateAdd);
    }
}
