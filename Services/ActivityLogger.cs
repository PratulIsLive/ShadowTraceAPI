using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Services;

public class ActivityLogger
{
    private readonly IActivityRepository _activityRepository;
    private readonly ICurrentUserService _currentUserService;

    public ActivityLogger(
        IActivityRepository activityRepository,
        ICurrentUserService currentUserService)
    {
        _activityRepository = activityRepository;
        _currentUserService = currentUserService;
    }

    public async Task LogAsync(
        int caseId,
        string module,
        string entityName,
        string actionType,
        string description)
    {
        var activity = new ActivityLog
        {
            InvestigationCaseId = caseId,
            PerformedById = _currentUserService.UserId,
            Module = module,
            EntityName = entityName,
            ActionType = actionType,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };

        await _activityRepository.CreateAsync(activity);
    }
}