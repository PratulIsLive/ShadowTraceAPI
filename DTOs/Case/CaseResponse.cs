using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Case;

public class CaseResponse
{
    public int Id { get; set; }

    public string CaseNumber { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public DateTime IncidentDate { get; set; }

    public string ReporterName { get; set; } = string.Empty;

    public PriorityLevel Priority { get; set; }

    public CaseStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}