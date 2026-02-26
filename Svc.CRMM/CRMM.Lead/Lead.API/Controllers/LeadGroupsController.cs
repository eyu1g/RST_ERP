using Lead.App.Service;
using Lead.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/[controller]")]
public sealed class LeadGroupsController : ControllerBase
{
    private readonly LeadGroupService _service;
    private readonly LeadGroupSettingsService _settings;

    public LeadGroupsController(LeadGroupService service, LeadGroupSettingsService settings)
    {
        _service = service;
        _settings = settings;
    }

    [HttpGet("settings")]
    public ActionResult<LeadGroupSettingsDto> GetSettings()
    {
        return Ok(_settings.GetSettings());
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadGroupDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _service.ListAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<object>> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(new { group = result.Value.Group, conditions = result.Value.Conditions });
    }

    [HttpPost]
    public async Task<ActionResult<LeadGroupDto>> Create([FromBody] CreateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadGroupDto>> Update(Guid id, [FromBody] UpdateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _service.DeleteAsync(id, cancellationToken);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("{leadGroupId:guid}/conditions")]
    public async Task<ActionResult<IReadOnlyList<LeadGroupConditionDto>>> ListConditions(Guid leadGroupId, CancellationToken cancellationToken)
    {
        var items = await _service.ListConditionsAsync(leadGroupId, cancellationToken);
        return Ok(items);
    }

    [HttpPost("{leadGroupId:guid}/conditions")]
    public async Task<ActionResult<LeadGroupConditionDto>> CreateCondition(Guid leadGroupId, [FromBody] CreateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var created = await _service.CreateConditionAsync(leadGroupId, request, cancellationToken);
        return Ok(created);
    }

    [HttpPut("conditions/{id:guid}")]
    public async Task<ActionResult<LeadGroupConditionDto>> UpdateCondition(Guid id, [FromBody] UpdateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateConditionAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("conditions/{id:guid}")]
    public async Task<IActionResult> DeleteCondition(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _service.DeleteConditionAsync(id, cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
