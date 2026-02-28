using Lead.Api.Mediator;
using Lead.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/lead-scoring-rules")]
public sealed class LeadScoringRulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadScoringRulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadScoringRuleDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new AdminRequests.LeadScoringRules.ListQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LeadScoringRuleDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new AdminRequests.LeadScoringRules.GetQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LeadScoringRuleDto>> Create([FromBody] CreateLeadScoringRuleRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new AdminRequests.LeadScoringRules.CreateCommand(request), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadScoringRuleDto>> Update(Guid id, [FromBody] UpdateLeadScoringRuleRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadScoringRules.UpdateCommand(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/activate")]
    public async Task<ActionResult<LeadScoringRuleDto>> Activate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadScoringRules.SetActiveCommand(id, true), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<ActionResult<LeadScoringRuleDto>> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadScoringRules.SetActiveCommand(id, false), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _mediator.Send(new AdminRequests.LeadScoringRules.DeleteCommand(id), cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
