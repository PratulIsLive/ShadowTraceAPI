using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Dashboard;

public class DashboardResponse
{
    public int TotalCases { get; set; }

    public int OpenCases { get; set; }

    public int ClosedCases { get; set; }

    public int TotalSuspects { get; set; }

    public int TotalEvidence { get; set; }

    public List<DashboardCaseStatusResponse> CasesByPriority { get; set; } = new();

    public List<RecentCaseResponse> RecentCases { get; set; } = new();
    
}