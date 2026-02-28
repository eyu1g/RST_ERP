using Lead.Api.Mediator;
using Lead.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/lookups")]
public sealed class LookupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LookupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("statuses")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListStatuses(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new LookupsRequests.ListLeadStatusesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("sources")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListSources(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new LookupsRequests.ListLeadSourcesQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("stages")]
    public async Task<ActionResult<IReadOnlyList<LookupItemDto>>> ListStages(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new LookupsRequests.ListLeadStagesQuery(), cancellationToken);
        return Ok(items);
    }
}
