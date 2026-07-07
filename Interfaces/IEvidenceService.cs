using ShadowTraceAPI.DTOs.Evidence;

namespace ShadowTraceAPI.Interfaces;

public interface IEvidenceService
{
    Task<EvidenceResponse> CreateAsync(CreateEvidenceRequest request);

    Task<List<EvidenceSummaryResponse>> GetAllAsync();

    Task<EvidenceResponse?> GetByIdAsync(int id);

    Task<EvidenceResponse?> UpdateAsync(int id, UpdateEvidenceRequest request);

    Task<bool> DeleteAsync(int id);

    Task<List<EvidenceSummaryResponse>> GetByCaseIdAsync(int caseId);
}