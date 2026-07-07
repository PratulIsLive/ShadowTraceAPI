using ShadowTraceAPI.DTOs.Activity;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Services;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _repository;

    public ActivityService(IActivityRepository repository)
    {
        _repository = repository;
    }

    public async Task<ActivityResponse> CreateAsync(CreateActivityRequest request)
    {
        var activity = new ActivityLog
        {
            InvestigationCaseId = request.InvestigationCaseId,

            // Temporary until JWT user extraction
            PerformedById = 1,

            Module = request.Module,
            EntityName = request.EntityName,
            ActionType = request.ActionType,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        activity = await _repository.CreateAsync(activity);

        return MapToResponse(activity);
    }

    public async Task<List<ActivitySummaryResponse>> GetAllAsync()
    {
        var activities = await _repository.GetAllAsync();

        return activities.Select(a => new ActivitySummaryResponse
        {
            Id = a.Id,
            Module = a.Module,
            ActionType = a.ActionType,
            CreatedAt = a.CreatedAt
        }).ToList();
    }

    public async Task<ActivityResponse?> GetByIdAsync(int id)
    {
        var activity = await _repository.GetByIdAsync(id);

        if (activity == null)
            return null;

        return MapToResponse(activity);
    }

    public async Task<List<ActivitySummaryResponse>> GetByCaseIdAsync(int caseId)
    {
        var activities = await _repository.GetByCaseIdAsync(caseId);

        return activities.Select(a => new ActivitySummaryResponse
        {
            Id = a.Id,
            Module = a.Module,
            ActionType = a.ActionType,
            CreatedAt = a.CreatedAt
        }).ToList();
    }

    private static ActivityResponse MapToResponse(ActivityLog activity)
    {
        return new ActivityResponse
        {
            Id = activity.Id,
            InvestigationCaseId = activity.InvestigationCaseId,
            PerformedById = activity.PerformedById,
            Module = activity.Module,
            EntityName = activity.EntityName,
            ActionType = activity.ActionType,
            Description = activity.Description,
            CreatedAt = activity.CreatedAt
        };
    }
}