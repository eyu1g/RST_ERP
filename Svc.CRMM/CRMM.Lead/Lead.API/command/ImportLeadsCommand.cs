using Lead.App.Service;
using Lead.Domain.DTO;

namespace Lead.Api.Command;

public sealed class ImportLeadsCommand
{
    private readonly LeadImportService _service;

    public ImportLeadsCommand(LeadImportService service)
    {
        _service = service;
    }

    public Task<ImportLeadsResultDto> ExecuteAsync(Stream csvStream, CancellationToken cancellationToken)
    {
        return _service.ImportCsvAsync(csvStream, cancellationToken);
    }
}
