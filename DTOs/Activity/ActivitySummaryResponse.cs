namespace ShadowTraceAPI.DTOs.Activity;

public class ActivitySummaryResponse
{
    public int Id { get; set; }

    public string Module { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}