using Lead.Domain.DTO;
using Lead.Api.Command;
using Lead.Api.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/[controller]")]
public sealed class LeadsController : ControllerBase
{
    private readonly LeadQueries _queries;
    private readonly CreateLeadCommand _create;
    private readonly UpdateLeadCommand _update;
    private readonly ChangeLeadStatusCommand _changeStatus;
    private readonly AddLeadScoreCommand _addScore;
    private readonly ConvertLeadCommand _convert;
    private readonly AssignLeadCommand _assign;
    private readonly DeleteLeadCommand _delete;
    private readonly ImportLeadsCommand _import;

    public LeadsController(
        LeadQueries queries,
        CreateLeadCommand create,
        UpdateLeadCommand update,
        ChangeLeadStatusCommand changeStatus,
        AddLeadScoreCommand addScore,
        ConvertLeadCommand convert,
        AssignLeadCommand assign,
        DeleteLeadCommand delete,
        ImportLeadsCommand import)
    {
        _queries = queries;
        _create = create;
        _update = update;
        _changeStatus = changeStatus;
        _addScore = addScore;
        _convert = convert;
        _assign = assign;
        _delete = delete;
        _import = import;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _queries.ListAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("assigned")]
    public async Task<ActionResult<IReadOnlyList<AssignedLeadRowDto>>> ListAssigned(CancellationToken cancellationToken)
    {
        var items = await _queries.ListAssignedAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LeadDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var lead = await _queries.GetAsync(id, cancellationToken);
        return lead is null ? NotFound() : Ok(lead);
    }

    [HttpGet("{id:guid}/detail")]
    public async Task<ActionResult<LeadDetailDto>> GetDetail(Guid id, CancellationToken cancellationToken)
    {
        var detail = await _queries.GetDetailAsync(id, cancellationToken);
        return detail is null ? NotFound() : Ok(detail);
    }

    [HttpPost]
    public async Task<ActionResult<LeadDto>> Create([FromBody] CreateLeadRequest request, CancellationToken cancellationToken)
    {
        var created = await _create.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpGet("import/template")]
    public IActionResult DownloadImportTemplate()
    {
        var header = string.Join(",", ImportLeadsTemplateDto.Headers);
        var content = header + "\n";
        return File(System.Text.Encoding.UTF8.GetBytes(content), "text/csv", "lead-import-template.csv");
    }

    [HttpPost("import")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<ImportLeadsResultDto>> Import([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        if (file.Length == 0)
        {
            return BadRequest(new ImportLeadsResultDto(0, 0, new[] { new ImportLeadRowErrorDto(1, "Empty file.") }));
        }

        await using var stream = file.OpenReadStream();
        var result = await _import.ExecuteAsync(stream, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadDto>> Update(Guid id, [FromBody] UpdateLeadRequest request, CancellationToken cancellationToken)
    {
        var updated = await _update.ExecuteAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/status")]
    public async Task<ActionResult<LeadDto>> ChangeStatus(Guid id, [FromBody] ChangeLeadStatusRequest request, CancellationToken cancellationToken)
    {
        var updated = await _changeStatus.ExecuteAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/score")]
    public async Task<ActionResult<LeadScoreHistoryDto>> AddScore(Guid id, [FromBody] UpdateLeadScoreRequest request, CancellationToken cancellationToken)
    {
        var created = await _addScore.ExecuteAsync(id, request, cancellationToken);
        return created is null ? NotFound() : Ok(created);
    }

    [HttpPost("{id:guid}/convert")]
    public async Task<ActionResult<ConvertLeadResultDto>> Convert(Guid id, [FromBody] ConvertLeadRequest request, CancellationToken cancellationToken)
    {
        var result = await _convert.ExecuteAsync(id, request, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("{id:guid}/assign")]
    public async Task<ActionResult<LeadDto>> Assign(Guid id, [FromBody] AssignLeadRequest request, CancellationToken cancellationToken)
    {
        var updated = await _assign.ExecuteAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _delete.ExecuteAsync(id, cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
