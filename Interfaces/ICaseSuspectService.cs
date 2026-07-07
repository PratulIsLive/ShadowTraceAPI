using ShadowTraceAPI.DTOs.CaseSuspect;

namespace ShadowTraceAPI.Interfaces;

public interface ICaseSuspectService
{
    Task<CaseSuspectResponse> AssignAsync(AssignSuspectRequest request);

    Task<IEnumerable<CaseSuspectSummaryResponse>> GetByCaseIdAsync(int caseId);

    Task<IEnumerable<CaseSuspectResponse>> GetBySuspectIdAsync(int suspectId);

    Task<bool> RemoveAsync(RemoveSuspectRequest request);
}