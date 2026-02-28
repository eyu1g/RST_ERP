using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class UpdateLeadSourceAdminCommand
{
    private readonly ILeadDbContext _db;

    public UpdateLeadSourceAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadSourceAdminDto?> ExecuteAsync(Guid id, UpdateLeadSourceRequest request, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadSources.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;

        entity.Name = request.Name;
        entity.SortOrder = request.SortOrder;
        entity.DateMod = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        return new LeadSourceAdminDto(entity.Id, entity.Name, entity.SortOrder, entity.IsActive, entity.DateAdd);
    }
}
