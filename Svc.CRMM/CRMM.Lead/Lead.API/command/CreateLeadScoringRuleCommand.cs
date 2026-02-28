using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Lead.Domain.Entities;

namespace Lead.Api.Command;

public sealed class CreateLeadScoringRuleCommand
{
    private readonly ILeadDbContext _db;

    public CreateLeadScoringRuleCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadScoringRuleDto> ExecuteAsync(CreateLeadScoringRuleRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var entity = new LeadScoringRule
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            MaxPoints = request.MaxPoints,
            WeightPercent = request.WeightPercent,
            IsActive = request.IsActive,
            SortOrder = request.SortOrder,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadScoringRules.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadScoringRuleDto(entity.Id, entity.Name, entity.MaxPoints, entity.WeightPercent, entity.IsActive, entity.DateAdd);
    }
}
