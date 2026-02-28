using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Lead.Domain.Entities;

namespace Lead.Api.Command;

public sealed class CreateLeadSourceAdminCommand
{
    private readonly ILeadDbContext _db;

    public CreateLeadSourceAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadSourceAdminDto> ExecuteAsync(CreateLeadSourceRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var entity = new LeadSource
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            SortOrder = request.SortOrder,
            IsActive = request.IsActive,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadSources.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadSourceAdminDto(entity.Id, entity.Name, entity.SortOrder, entity.IsActive, entity.DateAdd);
    }
}
