using ShadowTraceAPI.DTOs.Suspect;

namespace ShadowTraceAPI.Interfaces;

public interface ISuspectService
{
    Task<IEnumerable<SuspectSummaryResponse>> GetAllAsync();

    Task<SuspectResponse?> GetByIdAsync(int id);

    Task<SuspectResponse> CreateAsync(CreateSuspectRequest request);

    Task<SuspectResponse?> UpdateAsync(int id, UpdateSuspectRequest request);

    Task<bool> DeleteAsync(int id);
}