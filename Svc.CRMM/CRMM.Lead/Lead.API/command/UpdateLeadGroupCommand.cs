using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class UpdateLeadGroupCommand
{
    private readonly ILeadDbContext _db;

    public UpdateLeadGroupCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadGroupDto?> ExecuteAsync(Guid id, UpdateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadGroups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;

        var now = DateTime.UtcNow;
        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.IsActive = request.IsActive;
        entity.DateMod = now;

        await _db.SaveChangesAsync(cancellationToken);

        return new LeadGroupDto(entity.Id, entity.Code, entity.Name, entity.IsActive, 0, entity.DateAdd, entity.DateMod ?? entity.DateAdd);
    }
}
