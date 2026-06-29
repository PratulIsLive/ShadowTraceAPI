namespace ShadowTraceAPI.Entities;

public class CaseAssignment
{
    public int Id { get; set; }

    public int InvestigationCaseId { get; set; }

    public InvestigationCase InvestigationCase { get; set; } = null!;

    public int InvestigatorId { get; set; }

    public User Investigator { get; set; } = null!;

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UnassignedAt { get; set; }

    public string AssignmentReason { get; set; } = string.Empty;
}