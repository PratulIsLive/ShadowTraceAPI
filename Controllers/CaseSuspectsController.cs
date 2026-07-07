using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShadowTraceAPI.DTOs.CaseSuspect;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CaseSuspectsController : ControllerBase
{
    private readonly ICaseSuspectService _service;

    public CaseSuspectsController(ICaseSuspectService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CaseSuspectResponse>> Assign(
        AssignSuspectRequest request)
    {
        var response = await _service.AssignAsync(request);
        return Created(string.Empty, response);
    }

    [HttpGet("case/{caseId}")]
    public async Task<ActionResult<IEnumerable<CaseSuspectSummaryResponse>>> GetByCase(
        int caseId)
    {
        var response = await _service.GetByCaseIdAsync(caseId);
        return Ok(response);
    }

    [HttpGet("suspect/{suspectId}")]
    public async Task<ActionResult<IEnumerable<CaseSuspectResponse>>> GetBySuspect(
        int suspectId)
    {
        var response = await _service.GetBySuspectIdAsync(suspectId);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(RemoveSuspectRequest request)
    {
        var removed = await _service.RemoveAsync(request);

        if (!removed)
            return NotFound();

        return NoContent();
    }
}