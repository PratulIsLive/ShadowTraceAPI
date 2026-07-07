using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShadowTraceAPI.DTOs.Activity;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Controllers;

[ApiController]
[Route("api/activity")]
[Authorize(Roles = "Admin,Supervisor")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivityController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpPost]
    public async Task<ActionResult<ActivityResponse>> Create(CreateActivityRequest request)
    {
        var response = await _activityService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ActivitySummaryResponse>>> GetAll()
    {
        var response = await _activityService.GetAllAsync();

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ActivityResponse>> GetById(int id)
    {
        var response = await _activityService.GetByIdAsync(id);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet("case/{caseId:int}")]
    public async Task<ActionResult<List<ActivitySummaryResponse>>> GetByCaseId(int caseId)
    {
        var response = await _activityService.GetByCaseIdAsync(caseId);

        return Ok(response);
    }
}