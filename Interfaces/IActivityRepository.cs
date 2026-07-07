using ShadowTraceAPI.Entities;

namespace ShadowTraceAPI.Interfaces;

public interface IActivityRepository
{
    Task<ActivityLog> CreateAsync(ActivityLog activity);

    Task<List<ActivityLog>> GetAllAsync();

    Task<ActivityLog?> GetByIdAsync(int id);

    Task<List<ActivityLog>> GetByCaseIdAsync(int caseId);
}