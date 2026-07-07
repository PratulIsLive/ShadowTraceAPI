using ShadowTraceAPI.DTOs.Activity;

namespace ShadowTraceAPI.Interfaces;

public interface IActivityService
{
    Task<ActivityResponse> CreateAsync(CreateActivityRequest request);

    Task<List<ActivitySummaryResponse>> GetAllAsync();

    Task<ActivityResponse?> GetByIdAsync(int id);

    Task<List<ActivitySummaryResponse>> GetByCaseIdAsync(int caseId);
}