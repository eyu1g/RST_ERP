using Lead.Api.Mediator;
using Lead.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/[controller]")]
public sealed class LeadGroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadGroupsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("settings")]
    public ActionResult<LeadGroupSettingsDto> GetSettings()
    {
        var settings = _mediator.Send(new LeadGroupsRequests.GetLeadGroupSettingsQuery()).GetAwaiter().GetResult();
        return Ok(settings);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadGroupDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new LeadGroupsRequests.ListLeadGroupsQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<object>> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LeadGroupsRequests.GetLeadGroupQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(new { group = result.Value.Group, conditions = result.Value.Conditions });
    }

    [HttpPost]
    public async Task<ActionResult<LeadGroupDto>> Create([FromBody] CreateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new LeadGroupsRequests.CreateLeadGroupRequestMessage(request), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadGroupDto>> Update(Guid id, [FromBody] UpdateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new LeadGroupsRequests.UpdateLeadGroupRequestMessage(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _mediator.Send(new LeadGroupsRequests.DeleteLeadGroupRequestMessage(id), cancellationToken);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("{leadGroupId:guid}/conditions")]
    public async Task<ActionResult<IReadOnlyList<LeadGroupConditionDto>>> ListConditions(Guid leadGroupId, CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new LeadGroupsRequests.ListLeadGroupConditionsQuery(leadGroupId), cancellationToken);
        return Ok(items);
    }

    [HttpPost("{leadGroupId:guid}/conditions")]
    public async Task<ActionResult<LeadGroupConditionDto>> CreateCondition(Guid leadGroupId, [FromBody] CreateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new LeadGroupsRequests.CreateLeadGroupConditionRequestMessage(leadGroupId, request), cancellationToken);
        return Ok(created);
    }

    [HttpPut("conditions/{id:guid}")]
    public async Task<ActionResult<LeadGroupConditionDto>> UpdateCondition(Guid id, [FromBody] UpdateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new LeadGroupsRequests.UpdateLeadGroupConditionRequestMessage(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("conditions/{id:guid}")]
    public async Task<IActionResult> DeleteCondition(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _mediator.Send(new LeadGroupsRequests.DeleteLeadGroupConditionRequestMessage(id), cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
