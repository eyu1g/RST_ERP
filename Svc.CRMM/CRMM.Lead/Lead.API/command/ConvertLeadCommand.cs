using Lead.App.Interfaces;
using Lead.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lead.Api.Command;

public sealed class ConvertLeadCommand
{
    private readonly ILeadDbContext _db;

    private static readonly Guid ConvertedStatusId = Guid.Parse("11111111-1111-1111-1111-111111111116");
    private static readonly Guid ConversionStageId = Guid.Parse("22222222-2222-2222-2222-222222222226");

    public ConvertLeadCommand(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<ConvertLeadResultDto?> ExecuteAsync(Guid id, ConvertLeadRequest request, CancellationToken cancellationToken)
    {
        var lead = await _db.Leads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (lead is null) return null;

        var now = DateTime.UtcNow;

        var convertedToParts = new List<string>();
        if (request.CreateContact) convertedToParts.Add("Contact");
        if (request.CreateAccount) convertedToParts.Add("Account");
        if (request.CreateOpportunity) convertedToParts.Add("Opportunity");
        var convertedTo = convertedToParts.Count == 0 ? "Converted" : string.Join(",", convertedToParts);

        var log = new Lead.Domain.Entities.LeadConversionLog
        {
            Id = Guid.NewGuid(),
            LeadId = id,
            ConvertedTo = convertedTo,
            TargetId = null,
            ConvertedBy = request.ConvertedBy,
            ConvertedAt = now
        };

        _db.LeadConversionLogs.Add(log);

        lead.StatusId = ConvertedStatusId;
        lead.StageId = ConversionStageId;
        lead.DateMod = now;

        await _db.SaveChangesAsync(cancellationToken);

        return new ConvertLeadResultDto(
            LeadId: id,
            ConversionLogId: log.Id,
            StatusId: lead.StatusId,
            StageId: lead.StageId,
            ConvertedAt: log.ConvertedAt);
    }
}
