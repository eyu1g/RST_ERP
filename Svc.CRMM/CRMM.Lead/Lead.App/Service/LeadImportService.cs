using System.Globalization;
using System.Text;
using Lead.App.Helpers;
using Lead.App.Interfaces;
using Lead.Domain.DTO;
using LeadEntity = Lead.Domain.Entities.Lead;

namespace Lead.App.Service;

public sealed class LeadImportService
{
    private readonly ILeadDbContext _db;

    public LeadImportService(ILeadDbContext db)
    {
        _db = db;
    }

    public async Task<ImportLeadsResultDto> ImportCsvAsync(Stream csvStream, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(csvStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true);

        var headerLine = await reader.ReadLineAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(headerLine))
        {
            return new ImportLeadsResultDto(0, 0, new[] { new ImportLeadRowErrorDto(1, "Missing header row.") });
        }

        var headers = ParseCsvLine(headerLine).Select(h => h.Trim()).ToList();
        var headerIndex = headers
            .Select((h, i) => new { h, i })
            .ToDictionary(x => x.h, x => x.i, StringComparer.OrdinalIgnoreCase);

        foreach (var required in ImportLeadsTemplateDto.Headers)
        {
            if (!headerIndex.ContainsKey(required))
            {
                return new ImportLeadsResultDto(0, 0, new[] { new ImportLeadRowErrorDto(1, $"Missing required column '{required}'.") });
            }
        }

        var errors = new List<ImportLeadRowErrorDto>();
        var createdCount = 0;
        var totalRows = 0;

        string? line;
        var rowNumber = 1;

        while ((line = await reader.ReadLineAsync(cancellationToken)) is not null)
        {
            rowNumber++;
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            totalRows++;
            var values = ParseCsvLine(line);

            try
            {
                var firstName = GetString(values, headerIndex, "FirstName");
                var lastName = GetString(values, headerIndex, "LastName");
                var email = GetString(values, headerIndex, "Email");
                var phone = GetString(values, headerIndex, "Phone");
                var companyName = GetString(values, headerIndex, "CompanyName");
                var companySize = GetString(values, headerIndex, "CompanySize");
                var jobTitle = GetString(values, headerIndex, "JobTitle");
                var industry = GetString(values, headerIndex, "Industry");
                var budget = GetDecimal(values, headerIndex, "Budget");
                var timeline = GetString(values, headerIndex, "Timeline");

                var sourceId = GetGuid(values, headerIndex, "SourceId");
                var statusId = GetGuid(values, headerIndex, "StatusId");
                var stageId = GetGuid(values, headerIndex, "StageId");

                var assignedToUserId = GetGuidNullable(values, headerIndex, "AssignedToUserId");
                var assignedToName = GetString(values, headerIndex, "AssignedToName");

                var now = DateTime.UtcNow;

                var chosenSourceId = sourceId ?? _db.LeadSources.Select(x => x.Id).FirstOrDefault();
                var chosenStatusId = statusId ?? _db.LeadStatuses.OrderBy(x => x.SortOrder).Select(x => x.Id).FirstOrDefault();
                var chosenStageId = stageId ?? _db.LeadStages.OrderBy(x => x.SortOrder).Select(x => x.Id).FirstOrDefault();

                if (chosenSourceId == Guid.Empty)
                {
                    throw new InvalidOperationException("No LeadSource rows exist.");
                }

                if (chosenStatusId == Guid.Empty)
                {
                    throw new InvalidOperationException("No LeadStatus rows exist.");
                }

                if (chosenStageId == Guid.Empty)
                {
                    throw new InvalidOperationException("No LeadStage rows exist.");
                }

                var lead = new LeadEntity
                {
                    Id = Guid.NewGuid(),
                    LeadNo = LeadNoGenerator.NewLeadNo(now),
                    FirstName = firstName,
                    LastName = lastName,
                    CompanyName = companyName,
                    CompanySize = companySize,
                    JobTitle = jobTitle,
                    Industry = industry,
                    Budget = budget,
                    Timeline = timeline,
                    Email = email,
                    Phone = phone,
                    SourceId = chosenSourceId,
                    StatusId = chosenStatusId,
                    StageId = chosenStageId,
                    AssignedToUserId = assignedToUserId,
                    AssignedToName = assignedToName,
                    DateAdd = now,
                    DateMod = now
                };

                _db.Leads.Add(lead);
                createdCount++;
            }
            catch (Exception ex)
            {
                errors.Add(new ImportLeadRowErrorDto(rowNumber, ex.Message));
            }
        }

        if (createdCount > 0)
        {
            await _db.SaveChangesAsync(cancellationToken);
        }

        return new ImportLeadsResultDto(totalRows, createdCount, errors);
    }

    private static string? GetString(List<string> values, IReadOnlyDictionary<string, int> headerIndex, string key)
    {
        if (!headerIndex.TryGetValue(key, out var idx)) return null;
        if (idx < 0 || idx >= values.Count) return null;
        var v = values[idx].Trim();
        return string.IsNullOrWhiteSpace(v) ? null : v;
    }

    private static Guid? GetGuid(List<string> values, IReadOnlyDictionary<string, int> headerIndex, string key)
    {
        var v = GetString(values, headerIndex, key);
        if (string.IsNullOrWhiteSpace(v)) return null;
        if (!Guid.TryParse(v, out var g))
        {
            throw new InvalidOperationException($"Invalid GUID for '{key}'.");
        }

        return g;
    }

    private static Guid? GetGuidNullable(List<string> values, IReadOnlyDictionary<string, int> headerIndex, string key)
    {
        return GetGuid(values, headerIndex, key);
    }

    private static decimal? GetDecimal(List<string> values, IReadOnlyDictionary<string, int> headerIndex, string key)
    {
        var v = GetString(values, headerIndex, key);
        if (string.IsNullOrWhiteSpace(v)) return null;
        if (!decimal.TryParse(v, NumberStyles.Number, CultureInfo.InvariantCulture, out var d))
        {
            throw new InvalidOperationException($"Invalid decimal for '{key}'.");
        }

        return d;
    }

    private static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        var sb = new StringBuilder();
        var inQuotes = false;

        for (var i = 0; i < line.Length; i++)
        {
            var c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    sb.Append('"');
                    i++;
                    continue;
                }

                inQuotes = !inQuotes;
                continue;
            }

            if (c == ',' && !inQuotes)
            {
                result.Add(sb.ToString());
                sb.Clear();
                continue;
            }

            sb.Append(c);
        }

        result.Add(sb.ToString());
        return result;
    }
}
