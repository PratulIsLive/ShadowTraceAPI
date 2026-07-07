using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Suspect;

public class SuspectResponse
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public RiskLevel RiskLevel { get; set; }

    public DateTime CreatedAt { get; set; }
}