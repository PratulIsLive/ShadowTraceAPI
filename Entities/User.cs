using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.Entities;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public Role Role { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string EmployeeId { get; set; } = string.Empty;

    public DateTime? LastLoginAt { get; set; }

    // Navigation Properties
    public ICollection<CaseAssignment> CaseAssignments { get; set; } = new List<CaseAssignment>();

    public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public ICollection<Evidence> UploadedEvidence { get; set; } = new List<Evidence>();
}