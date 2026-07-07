using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.CaseSuspect;

public class CaseSuspectSummaryResponse
{
    public int SuspectId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public RiskLevel RiskLevel { get; set; }
}