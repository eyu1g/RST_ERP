using Lead.Api.Mediator;
using Lead.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/lead-statuses")]
public sealed class LeadStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadStatusAdminDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new AdminRequests.LeadStatuses.ListQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LeadStatusAdminDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new AdminRequests.LeadStatuses.GetQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LeadStatusAdminDto>> Create([FromBody] CreateLeadStatusRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new AdminRequests.LeadStatuses.CreateCommand(request), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadStatusAdminDto>> Update(Guid id, [FromBody] UpdateLeadStatusRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadStatuses.UpdateCommand(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/activate")]
    public async Task<ActionResult<LeadStatusAdminDto>> Activate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadStatuses.SetActiveCommand(id, true), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<ActionResult<LeadStatusAdminDto>> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadStatuses.SetActiveCommand(id, false), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _mediator.Send(new AdminRequests.LeadStatuses.DeleteCommand(id), cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
