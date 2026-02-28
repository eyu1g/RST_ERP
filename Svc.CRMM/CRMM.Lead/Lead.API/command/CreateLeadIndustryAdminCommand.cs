using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Lead.Domain.Entities;

namespace Lead.Api.Command;

public sealed class CreateLeadIndustryAdminCommand
{
    private readonly ILeadDbContext _db;

    public CreateLeadIndustryAdminCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadIndustryAdminDto> ExecuteAsync(CreateLeadIndustryRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var entity = new LeadIndustry
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            SortOrder = request.SortOrder,
            IsActive = request.IsActive,
            DateAdd = now,
            DateMod = now
        };

        _db.LeadIndustries.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return new LeadIndustryAdminDto(entity.Id, entity.Name, entity.SortOrder, entity.IsActive, entity.DateAdd);
    }
}
