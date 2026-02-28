using Lead.App.Interfaces;
using Lead.Domain.DTO;

namespace Lead.Api.Command;

public sealed class CreateLeadGroupCommand
{
    private readonly ILeadDbContext _db;

    public CreateLeadGroupCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadGroupDto> ExecuteAsync(CreateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var entity = new Lead.Domain.Entities.LeadGroup
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            IsActive = request.IsActive,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadGroups.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadGroupDto(entity.Id, entity.Code, entity.Name, entity.IsActive, 0, entity.DateAdd, entity.DateMod ?? entity.DateAdd);
    }
}
