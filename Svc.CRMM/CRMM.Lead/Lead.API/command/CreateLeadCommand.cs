using Lead.App.Helpers;
using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class CreateLeadCommand
{
    private readonly ILeadDbContext _db;

    public CreateLeadCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<LeadDto> ExecuteAsync(CreateLeadRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var sourceId = request.SourceId ?? await _db.LeadSources
            .AsNoTracking()
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        var statusId = request.StatusId ?? await _db.LeadStatuses
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        var stageId = request.StageId ?? await _db.LeadStages
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (statusId == Guid.Empty)
            throw new InvalidOperationException("No LeadStatus rows exist. Seed LeadStatus before creating a Lead.");

        if (stageId == Guid.Empty)
            throw new InvalidOperationException("No LeadStage rows exist. Seed LeadStage before creating a Lead.");

        if (sourceId == Guid.Empty)
            throw new InvalidOperationException("No LeadSource rows exist. Seed LeadSource before creating a Lead.");

        var lead = new Lead.Domain.Entities.Lead
        {
            Id = Guid.NewGuid(),
            LeadNo = LeadNoGenerator.NewLeadNo(now),
            FirstName = request.FirstName,
            LastName = request.LastName,
            CompanyName = request.CompanyName,
            CompanySize = request.CompanySize,
            JobTitle = request.JobTitle,
            Industry = request.Industry,
            Budget = request.Budget,
            Timeline = request.Timeline,
            Email = request.Email,
            Phone = request.Phone,
            SourceId = sourceId,
            StatusId = statusId,
            StageId = stageId,
            AssignedToUserId = null,
            AssignedToName = null,
            DateAdd = now,
            DateMod = now
        };

        _db.Leads.Add(lead);
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
