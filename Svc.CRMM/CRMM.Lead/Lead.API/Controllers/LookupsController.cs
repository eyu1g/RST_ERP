using Lead.Api.Queries;
using Lead.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/lookups")]
public sealed class LookupsController : ControllerBase
{
    private readonly LookupQueries _queries;

    public LookupsController(LookupQueries queries)
    {
        _queries = queries;
    }

    [HttpGet("statuses")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListStatuses(CancellationToken cancellationToken)
    {
        var items = await _queries.ListStatusesAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("sources")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListSources(CancellationToken cancellationToken)
    {
        var items = await _queries.ListSourcesAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("stages")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListStages(CancellationToken cancellationToken)
    {
        var items = await _queries.ListStagesAsync(cancellationToken);
        return Ok(items);
    }
}
