using Lead.Domain.DTO;
using Lead.App.Service;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/[controller]")]
public sealed class LeadsController : ControllerBase
{
    private readonly LeadService _service;
    private readonly LeadImportService _import;

    public LeadsController(LeadService service, LeadImportService import)
    {
        _service = service;
        _import = import;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _service.ListAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("assigned")]
    public async Task<ActionResult<IReadOnlyList<AssignedLeadRowDto>>> ListAssigned(CancellationToken cancellationToken)
    {
        var items = await _service.ListAssignedAsync(cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LeadDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var lead = await _service.GetAsync(id, cancellationToken);
        return lead is null ? NotFound() : Ok(lead);
    }

    [HttpGet("{id:guid}/detail")]
    public async Task<ActionResult<LeadDetailDto>> GetDetail(Guid id, CancellationToken cancellationToken)
    {
        var detail = await _service.GetDetailAsync(id, cancellationToken);
        return detail is null ? NotFound() : Ok(detail);
    }

    [HttpPost]
    public async Task<ActionResult<LeadDto>> Create([FromBody] CreateLeadRequest request, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(request, cancellationToken);
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
        var result = await _import.ImportCsvAsync(stream, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadDto>> Update(Guid id, [FromBody] UpdateLeadRequest request, CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/status")]
    public async Task<ActionResult<LeadDto>> ChangeStatus(Guid id, [FromBody] ChangeLeadStatusRequest request, CancellationToken cancellationToken)
    {
        var updated = await _service.ChangeStatusAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/score")]
    public async Task<ActionResult<LeadScoreHistoryDto>> AddScore(Guid id, [FromBody] UpdateLeadScoreRequest request, CancellationToken cancellationToken)
    {
        var created = await _service.AddScoreAsync(id, request, cancellationToken);
        return created is null ? NotFound() : Ok(created);
    }

    [HttpPost("{id:guid}/convert")]
    public async Task<ActionResult<ConvertLeadResultDto>> Convert(Guid id, [FromBody] ConvertLeadRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.ConvertAsync(id, request, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("{id:guid}/assign")]
    public async Task<ActionResult<LeadDto>> Assign(Guid id, [FromBody] AssignLeadRequest request, CancellationToken cancellationToken)
    {
        var updated = await _service.AssignAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _service.DeleteAsync(id, cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
