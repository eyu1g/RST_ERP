using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class UpdateLeadCommand
{
    private readonly ILeadDbContext _db;

    public UpdateLeadCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadDto?> ExecuteAsync(Guid id, UpdateLeadRequest request, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null) return null;

        var now = DateTime.UtcNow;
        lead.FirstName = request.FirstName;
        lead.LastName = request.LastName;
        lead.CompanyName = request.CompanyName;
        lead.CompanySize = request.CompanySize;
        lead.JobTitle = request.JobTitle;
        lead.Industry = request.Industry;
        lead.Budget = request.Budget;
        lead.Timeline = request.Timeline;
        lead.Email = request.Email;
        lead.Phone = request.Phone;
        lead.SourceId = request.SourceId;
        lead.StatusId = request.StatusId;
        lead.StageId = request.StageId;
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
