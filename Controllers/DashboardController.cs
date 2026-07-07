using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShadowTraceAPI.DTOs.Dashboard;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Controllers;

[ApiController]
[Route("api/dashboard")]
[Authorize(Roles = "Admin,Supervisor")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardResponse>> GetDashboard()
    {
        var response = await _dashboardService.GetDashboardAsync();
        return Ok(response);
    }
}