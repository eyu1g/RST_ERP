using Lead.Domain.DTO;
using Lead.Api.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lead.Api.Controllers;

[ApiController]
[Route("api/crmm/lead/v{version:apiVersion}/[controller]")]
public sealed class LeadsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LeadDto>>> List(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new LeadsRequests.ListLeadsQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("assigned")]
    public async Task<ActionResult<IReadOnlyList<AssignedLeadRowDto>>> ListAssigned(CancellationToken cancellationToken)
    {
        var items = await _mediator.Send(new LeadsRequests.ListAssignedLeadsQuery(), cancellationToken);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LeadDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var lead = await _mediator.Send(new LeadsRequests.GetLeadQuery(id), cancellationToken);
        return lead is null ? NotFound() : Ok(lead);
    }

    [HttpGet("{id:guid}/detail")]
    public async Task<ActionResult<LeadDetailDto>> GetDetail(Guid id, CancellationToken cancellationToken)
    {
        var detail = await _mediator.Send(new LeadsRequests.GetLeadDetailQuery(id), cancellationToken);
        return detail is null ? NotFound() : Ok(detail);
    }

    [HttpPost]
    public async Task<ActionResult<LeadDto>> Create([FromBody] CreateLeadRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new LeadsRequests.CreateLeadRequestMessage(request), cancellationToken);
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
        var result = await _mediator.Send(new LeadsRequests.ImportLeadsRequestMessage(stream), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LeadDto>> Update(Guid id, [FromBody] UpdateLeadRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new LeadsRequests.UpdateLeadRequestMessage(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/status")]
    public async Task<ActionResult<LeadDto>> ChangeStatus(Guid id, [FromBody] ChangeLeadStatusRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new LeadsRequests.ChangeLeadStatusRequestMessage(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPost("{id:guid}/score")]
    public async Task<ActionResult<LeadScoreHistoryDto>> AddScore(Guid id, [FromBody] UpdateLeadScoreRequest request, CancellationToken cancellationToken)
    {
        var created = await _mediator.Send(new LeadsRequests.AddLeadScoreRequestMessage(id, request), cancellationToken);
        return created is null ? NotFound() : Ok(created);
    }

    [HttpPost("{id:guid}/convert")]
    public async Task<ActionResult<ConvertLeadResultDto>> Convert(Guid id, [FromBody] ConvertLeadRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LeadsRequests.ConvertLeadRequestMessage(id, request), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("{id:guid}/assign")]
    public async Task<ActionResult<LeadDto>> Assign(Guid id, [FromBody] AssignLeadRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mediator.Send(new LeadsRequests.AssignLeadRequestMessage(id, request), cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _mediator.Send(new LeadsRequests.DeleteLeadRequestMessage(id), cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
