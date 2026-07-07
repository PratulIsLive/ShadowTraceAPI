using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Case;

public class CaseSummaryResponse
{
    public int Id { get; set; }

    public string CaseNumber { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public PriorityLevel Priority { get; set; }

    public CaseStatus Status { get; set; }
}