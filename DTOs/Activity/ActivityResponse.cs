namespace ShadowTraceAPI.DTOs.Activity;

public class ActivityResponse
{
    public int Id { get; set; }

    public int InvestigationCaseId { get; set; }

    public int PerformedById { get; set; }

    public string Module { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}