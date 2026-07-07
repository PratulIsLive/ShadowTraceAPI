using ShadowTraceAPI.DTOs.Dashboard;

namespace ShadowTraceAPI.Interfaces;

public interface IDashboardService
{
    Task<DashboardResponse> GetDashboardAsync();
}