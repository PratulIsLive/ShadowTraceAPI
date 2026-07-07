using Microsoft.AspNetCore.Mvc;
using ShadowTraceAPI.DTOs.Case;
using ShadowTraceAPI.DTOs.Common;
using ShadowTraceAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace ShadowTraceAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/cases")]
public class CaseController : ControllerBase
{
    private readonly CaseService _caseService;

    public CaseController(CaseService caseService)
    {
        _caseService = caseService;
    }

    
    [Authorize(Roles = "Admin,Supervisor,Investigator")]
    [HttpPost]
    public async Task<ActionResult<CaseResponse>> Create(CreateCaseRequest request)
    {
        var response = await _caseService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<CaseSummaryResponse>>> GetAll(
        [FromQuery] PaginationRequest request)
    {
        var response = await _caseService.GetAllAsync(request);

        return Ok(response);
    }

    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CaseResponse>> GetById(int id)
    {
        var response = await _caseService.GetByIdAsync(id);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    
    [Authorize(Roles = "Admin,Supervisor,Investigator")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateCaseRequest request)
    {
        var updated = await _caseService.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _caseService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}