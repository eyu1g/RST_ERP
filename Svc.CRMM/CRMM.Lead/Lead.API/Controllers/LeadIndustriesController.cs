using Lead.Api.Mediator;
using Lead.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/lead-industries")]
public sealed class LeadIndustriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadIndustriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadIndustryAdminDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new AdminRequests.LeadIndustries.ListQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LeadIndustryAdminDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var item = await _mediator.Send(new AdminRequests.LeadIndustries.GetQuery(id), cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<LeadIndustryAdminDto>> Create([FromBody] CreateLeadIndustryRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new AdminRequests.LeadIndustries.CreateCommand(request), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadIndustryAdminDto>> Update(Guid id, [FromBody] UpdateLeadIndustryRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadIndustries.UpdateCommand(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/activate")]
    public async Task<ActionResult<LeadIndustryAdminDto>> Activate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadIndustries.SetActiveCommand(id, true), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/deactivate")]
    public async Task<ActionResult<LeadIndustryAdminDto>> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new AdminRequests.LeadIndustries.SetActiveCommand(id, false), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _mediator.Send(new AdminRequests.LeadIndustries.DeleteCommand(id), cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
