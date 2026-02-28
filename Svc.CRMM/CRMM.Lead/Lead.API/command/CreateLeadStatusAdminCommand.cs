using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Lead.Domain.Entities;

namespace Lead.Api.Command;

public sealed class CreateLeadStatusAdminCommand
{
    private readonly ILeadDbContext _db;

    public CreateLeadStatusAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadStatusAdminDto> ExecuteAsync(CreateLeadStatusRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var entity = new LeadStatus
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Priority = request.Priority,
            IsActive = request.IsActive,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadStatuses.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadStatusAdminDto(entity.Id, entity.Name, entity.Priority, entity.IsActive, entity.DateAdd);
    }
}
