using ShadowTraceAPI.Entities;

namespace ShadowTraceAPI.Interfaces;

public interface ICaseSuspectRepository
{
    Task<CaseSuspect> AddAsync(CaseSuspect caseSuspect);

    Task<bool> ExistsAsync(int caseId, int suspectId);

    Task<IEnumerable<CaseSuspect>> GetByCaseIdAsync(int caseId);

    Task<IEnumerable<CaseSuspect>> GetBySuspectIdAsync(int suspectId);

    Task<bool> RemoveAsync(int caseId, int suspectId);

    Task<bool> InvestigationCaseExistsAsync(int caseId);

    Task<bool> SuspectExistsAsync(int suspectId);
}