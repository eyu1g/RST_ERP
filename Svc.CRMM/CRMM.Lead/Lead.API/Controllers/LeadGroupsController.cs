using Lead.Api.Command;
using Lead.Api.Queries;
using Lead.App.Service;
using Lead.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/[controller]")]
public sealed class LeadGroupsController : ControllerBase
{
    private readonly LeadGroupQueries _queries;
    private readonly CreateLeadGroupCommand _create;
    private readonly UpdateLeadGroupCommand _update;
    private readonly DeleteLeadGroupCommand _delete;
    private readonly CreateLeadGroupConditionCommand _createCondition;
    private readonly UpdateLeadGroupConditionCommand _updateCondition;
    private readonly DeleteLeadGroupConditionCommand _deleteCondition;
    private readonly LeadGroupSettingsService _settings;

    public LeadGroupsController(
        LeadGroupQueries queries,
        CreateLeadGroupCommand create,
        UpdateLeadGroupCommand update,
        DeleteLeadGroupCommand delete,
        CreateLeadGroupConditionCommand createCondition,
        UpdateLeadGroupConditionCommand updateCondition,
        DeleteLeadGroupConditionCommand deleteCondition,
        LeadGroupSettingsService settings)
    {
        _queries = queries;
        _create = create;
        _update = update;
        _delete = delete;
        _createCondition = createCondition;
        _updateCondition = updateCondition;
        _deleteCondition = deleteCondition;
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
        var items = await _queries.ListAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<object>> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _queries.GetAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(new { group = result.Value.Group, conditions = result.Value.Conditions });
    }

    [HttpPost]
    public async Task<ActionResult<LeadGroupDto>> Create([FromBody] CreateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var created = await _create.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadGroupDto>> Update(Guid id, [FromBody] UpdateLeadGroupRequest request, CancellationToken cancellationToken)
    {
        var updated = await _update.ExecuteAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _delete.ExecuteAsync(id, cancellationToken);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("{leadGroupId:guid}/conditions")]
    public async Task<ActionResult<IReadOnlyList<LeadGroupConditionDto>>> ListConditions(Guid leadGroupId, CancellationToken cancellationToken)
    {
        var items = await _queries.ListConditionsAsync(leadGroupId, cancellationToken);
        return Ok(items);
    }

    [HttpPost("{leadGroupId:guid}/conditions")]
    public async Task<ActionResult<LeadGroupConditionDto>> CreateCondition(Guid leadGroupId, [FromBody] CreateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var created = await _createCondition.ExecuteAsync(leadGroupId, request, cancellationToken);
        return Ok(created);
    }

    [HttpPut("conditions/{id:guid}")]
    public async Task<ActionResult<LeadGroupConditionDto>> UpdateCondition(Guid id, [FromBody] UpdateLeadGroupConditionRequest request, CancellationToken cancellationToken)
    {
        var updated = await _updateCondition.ExecuteAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("conditions/{id:guid}")]
    public async Task<IActionResult> DeleteCondition(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _deleteCondition.ExecuteAsync(id, cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
