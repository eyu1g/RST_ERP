using Lead.Api.Mediator;
using Lead.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/lead-routing-rules")]
public sealed class LeadRoutingRulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadRoutingRulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadRoutingRuleDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new AdminRequests.LeadRoutingRules.ListQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LeadRoutingRuleDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new AdminRequests.LeadRoutingRules.GetQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LeadRoutingRuleDto>> Create([FromBody] CreateLeadRoutingRuleRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new AdminRequests.LeadRoutingRules.CreateCommand(request), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadRoutingRuleDto>> Update(Guid id, [FromBody] UpdateLeadRoutingRuleRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadRoutingRules.UpdateCommand(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/activate")]
    public async Task<ActionResult<LeadRoutingRuleDto>> Activate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadRoutingRules.SetActiveCommand(id, true), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<ActionResult<LeadRoutingRuleDto>> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadRoutingRules.SetActiveCommand(id, false), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _mediator.Send(new AdminRequests.LeadRoutingRules.DeleteCommand(id), cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
