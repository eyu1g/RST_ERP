using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class SetLeadStatusActiveAdminCommand
{
    private readonly ILeadDbContext _db;

    public SetLeadStatusActiveAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadStatusAdminDto?> ExecuteAsync(Guid id, bool isActive, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadStatuses.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;

        entity.IsActive = isActive;
        entity.DateMod = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        return new LeadStatusAdminDto(entity.Id, entity.Name, entity.Priority, entity.IsActive, entity.DateAdd);
    }
}
