using ShadowTraceAPI.Enums;
namespace ShadowTraceAPI.DTOs.Suspect;

public class SuspectSummaryResponse
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public RiskLevel RiskLevel { get; set; }
}