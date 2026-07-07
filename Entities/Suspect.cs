using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.Entities;

public class Suspect
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public RiskLevel RiskLevel { get; set; } = RiskLevel.Low;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int RiskScore { get; set; }

    // Navigation Property
    public ICollection<CaseSuspect> CaseSuspects { get; set; } = new List<CaseSuspect>();
}