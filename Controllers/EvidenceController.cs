using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShadowTraceAPI.DTOs.Evidence;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Controllers;

[ApiController]
[Route("api/evidence")]
[Authorize]
public class EvidenceController : ControllerBase
{
    private readonly IEvidenceService _evidenceService;

    public EvidenceController(IEvidenceService evidenceService)
    {
        _evidenceService = evidenceService;
    }

    // Admin, Supervisor and Investigator can upload evidence
    [Authorize(Roles = "Admin,Supervisor,Investigator")]
    [HttpPost]
    public async Task<ActionResult<EvidenceResponse>> Create(
        [FromForm] CreateEvidenceRequest request)
    {
        var response = await _evidenceService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    // Any authenticated user can view all evidence
    [HttpGet]
    public async Task<ActionResult<List<EvidenceSummaryResponse>>> GetAll()
    {
        var response = await _evidenceService.GetAllAsync();

        return Ok(response);
    }

    // Any authenticated user can view evidence by id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EvidenceResponse>> GetById(int id)
    {
        var response = await _evidenceService.GetByIdAsync(id);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    // Any authenticated user can view evidence belonging to a case
    [HttpGet("case/{caseId:int}")]
    public async Task<ActionResult<List<EvidenceSummaryResponse>>> GetByCaseId(int caseId)
    {
        var response = await _evidenceService.GetByCaseIdAsync(caseId);

        return Ok(response);
    }

    // Admin, Supervisor and Investigator can update evidence details
    [Authorize(Roles = "Admin,Supervisor,Investigator")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EvidenceResponse>> Update(
        int id,
        UpdateEvidenceRequest request)
    {
        var response = await _evidenceService.UpdateAsync(id, request);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    // Admin and Supervisor can delete evidence
    [Authorize(Roles = "Admin,Supervisor")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _evidenceService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}