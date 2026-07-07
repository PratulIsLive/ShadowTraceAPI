using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Dashboard;

public class DashboardCaseStatusResponse
{
    public PriorityLevel Priority { get; set; }

    public int Count { get; set; }
}