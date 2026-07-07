using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.Entities;

public class Evidence
{
    public int Id { get; set; }

    public int InvestigationCaseId { get; set; }

    public InvestigationCase InvestigationCase { get; set; } = null!;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public EvidenceType EvidenceType { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string StoragePath { get; set; } = string.Empty;

    public int UploadedById { get; set; }

    public User UploadedBy { get; set; } = null!;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public string EvidenceNumber { get; set; } = string.Empty;

    public string StorageLocation { get; set; } = string.Empty;

    public string CurrentHolder { get; set; } = string.Empty;

    public EvidenceStatus Status { get; set; }


}