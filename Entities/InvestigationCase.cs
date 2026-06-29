using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.Entities;

public class InvestigationCase
{
    public int Id { get; set; }

    public string CaseNumber { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public DateTime IncidentDate { get; set; }

    public string ReporterName { get; set; } = string.Empty;

    public PriorityLevel Priority { get; set; }

    public CaseStatus Status { get; set; } = CaseStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<CaseAssignment> CaseAssignments { get; set; } = new List<CaseAssignment>();

    public ICollection<Evidence> Evidence { get; set; } = new List<Evidence>();

    public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public ICollection<CaseSuspect> CaseSuspects { get; set; } = new List<CaseSuspect>();
}