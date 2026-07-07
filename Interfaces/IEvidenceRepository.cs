using ShadowTraceAPI.Entities;

namespace ShadowTraceAPI.Interfaces;

public interface IEvidenceRepository
{
    Task<Evidence> CreateAsync(Evidence evidence);

    Task<List<Evidence>> GetAllAsync();

    Task<Evidence?> GetByIdAsync(int id);

    Task<List<Evidence>> GetByCaseIdAsync(int caseId);

    Task UpdateAsync(Evidence evidence);

    Task DeleteAsync(Evidence evidence);

    Task<bool> InvestigationCaseExistsAsync(int caseId);
}