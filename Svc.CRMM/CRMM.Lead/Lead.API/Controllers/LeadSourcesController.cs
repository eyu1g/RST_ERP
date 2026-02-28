using Lead.Api.Mediator;
using Lead.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/lead-sources")]
public sealed class LeadSourcesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadSourcesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadSourceAdminDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new AdminRequests.LeadSources.ListQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LeadSourceAdminDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new AdminRequests.LeadSources.GetQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LeadSourceAdminDto>> Create([FromBody] CreateLeadSourceRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new AdminRequests.LeadSources.CreateCommand(request), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadSourceAdminDto>> Update(Guid id, [FromBody] UpdateLeadSourceRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadSources.UpdateCommand(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/activate")]
    public async Task<ActionResult<LeadSourceAdminDto>> Activate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadSources.SetActiveCommand(id, true), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<ActionResult<LeadSourceAdminDto>> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadSources.SetActiveCommand(id, false), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _mediator.Send(new AdminRequests.LeadSources.DeleteCommand(id), cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
