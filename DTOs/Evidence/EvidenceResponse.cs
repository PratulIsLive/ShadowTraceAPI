using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Evidence;

public class EvidenceResponse
{
    public int Id { get; set; }

    public int InvestigationCaseId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public EvidenceType EvidenceType { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string StoragePath { get; set; } = string.Empty;

    public int UploadedById { get; set; }

    public DateTime UploadedAt { get; set; }
}