using Lead.App.Service;
using Lead.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/lookups")]
public sealed class LookupsController : ControllerBase
{
    private readonly LeadLookupService _service;

    public LookupsController(LeadLookupService service)
    {
        _service = service;
    }

    [HttpGet("statuses")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListStatuses(CancellationToken cancellationToken)
    {
        var items = await _service.ListStatusesAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("sources")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListSources(CancellationToken cancellationToken)
    {
        var items = await _service.ListSourcesAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("stages")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListStages(CancellationToken cancellationToken)
    {
        var items = await _service.ListStagesAsync(cancellationToken);
        return Ok(items);
    }
}
