using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShadowTraceAPI.DTOs.Suspect;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SuspectsController : ControllerBase
{
    private readonly ISuspectService _service;

    public SuspectsController(ISuspectService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var suspects = await _service.GetAllAsync();
        return Ok(suspects);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var suspect = await _service.GetByIdAsync(id);

        if (suspect == null)
            return NotFound();

        return Ok(suspect);
    }

    [Authorize(Roles = "Admin,Supervisor,Investigator")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateSuspectRequest request)
    {
        var suspect = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = suspect.Id },
            suspect);
    }

    [Authorize(Roles = "Admin,Supervisor")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateSuspectRequest request)
    {
        var suspect = await _service.UpdateAsync(id, request);

        if (suspect == null)
            return NotFound();

        return Ok(suspect);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}