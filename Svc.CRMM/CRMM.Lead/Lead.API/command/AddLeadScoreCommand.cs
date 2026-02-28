using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class AddLeadScoreCommand
{
    private readonly ILeadDbContext _db;

    public AddLeadScoreCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadScoreHistoryDto?> ExecuteAsync(Guid id, UpdateLeadScoreRequest request, CancellationToken cancellationToken)
    {
        var leadExists = await _db.Leads
            .AsNoTracking()
            .AnyAsync(x => x.Id == id, cancellationToken);

        if (!leadExists) return null;

        var now = DateTime.UtcNow;
        var entity = new Lead.Domain.Entities.LeadScoreHistory
        {
            Id = Guid.NewGuid(),
            LeadId = id,
            Score = request.Score,
            Reason = request.Reason,
            ScoredAt = now
        };

        _db.LeadScoreHistories.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadScoreHistoryDto(entity.Id, entity.LeadId, entity.Score, entity.Reason, entity.ScoredAt);
    }
}
