using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class AssignLeadCommand
{
    private readonly ILeadDbContext _db;

    public AssignLeadCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadDto?> ExecuteAsync(Guid id, AssignLeadRequest request, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null) return null;

        var now = DateTime.UtcNow;
        lead.AssignedToUserId = request.AssignedToUserId;
        lead.AssignedToName = request.AssignedToName;
        lead.DateMod = now;

        await _db.SaveChangesAsync(cancellationToken);

        return new LeadDto(
            lead.Id,
            lead.LeadNo,
            lead.FirstName,
            lead.LastName,
            lead.CompanyName,
            lead.CompanySize,
            lead.JobTitle,
            lead.Industry,
            lead.Budget,
            lead.Timeline,
            lead.Email,
            lead.Phone,
            lead.SourceId,
            lead.StatusId,
            lead.StageId,
            lead.AssignedToUserId,
            lead.AssignedToName,
            lead.DateAdd,
            lead.DateMod ?? lead.DateAdd);
    }
}
