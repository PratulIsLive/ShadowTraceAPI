using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Evidence;

public class EvidenceSummaryResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public EvidenceType EvidenceType { get; set; }

    public DateTime UploadedAt { get; set; }
}