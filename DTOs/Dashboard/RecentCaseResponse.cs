using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Dashboard;

public class RecentCaseResponse
{
    public int Id { get; set; }

    public string CaseNumber { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public PriorityLevel Priority { get; set; }

    public CaseStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}