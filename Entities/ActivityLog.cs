namespace ShadowTraceAPI.Entities;

public class ActivityLog
{
    public int Id { get; set; }

    public int InvestigationCaseId { get; set; }

    public InvestigationCase InvestigationCase { get; set; } = null!;

    public int PerformedById { get; set; }

    public User PerformedBy { get; set; } = null!;

    public string Module { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}