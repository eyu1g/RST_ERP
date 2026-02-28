using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class UpdateLeadIndustryAdminCommand
{
    private readonly ILeadDbContext _db;

    public UpdateLeadIndustryAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadIndustryAdminDto?> ExecuteAsync(Guid id, UpdateLeadIndustryRequest request, CancellationToken cancellationToken)
    {
        var entity = await _db.LeadIndustries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;

        entity.Name = request.Name;
        entity.SortOrder = request.SortOrder;
        entity.DateMod = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        return new LeadIndustryAdminDto(entity.Id, entity.Name, entity.SortOrder, entity.IsActive, entity.DateAdd);
    }
}
