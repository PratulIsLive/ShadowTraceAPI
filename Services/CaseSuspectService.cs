using ShadowTraceAPI.DTOs.CaseSuspect;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;
using ShadowTraceAPI.Exceptions;

namespace ShadowTraceAPI.Services;

public class CaseSuspectService : ICaseSuspectService
{
    private readonly ICaseSuspectRepository _repository;
    private readonly ActivityLogger _activityLogger;

    public CaseSuspectService(
        ICaseSuspectRepository repository,
        ActivityLogger activityLogger)
    {
        _repository = repository;
        _activityLogger = activityLogger;
    }

    public async Task<CaseSuspectResponse> AssignAsync(AssignSuspectRequest request)
    {
        if (!await _repository.InvestigationCaseExistsAsync(request.InvestigationCaseId))
            throw new NotFoundException("Investigation Case not found.");

        if (!await _repository.SuspectExistsAsync(request.SuspectId))
            throw new NotFoundException("Suspect not found.");

        if (await _repository.ExistsAsync(request.InvestigationCaseId, request.SuspectId))
            throw new ConflictException("Suspect is already assigned to this case.");

        var entity = new CaseSuspect
        {
            InvestigationCaseId = request.InvestigationCaseId,
            SuspectId = request.SuspectId
        };

        entity = await _repository.AddAsync(entity);

        await _activityLogger.LogAsync(
            entity.InvestigationCaseId,
            "Case-Suspect",
            $"Suspect {entity.SuspectId}",
            "Assign",
            $"Suspect {entity.SuspectId} assigned to Case {entity.InvestigationCaseId}.");

        return new CaseSuspectResponse
        {
            InvestigationCaseId = entity.InvestigationCaseId,
            SuspectId = entity.SuspectId
        };
    }

    public async Task<IEnumerable<CaseSuspectSummaryResponse>> GetByCaseIdAsync(int caseId)
    {
        var list = await _repository.GetByCaseIdAsync(caseId);

        return list.Select(cs => new CaseSuspectSummaryResponse
        {
            SuspectId = cs.SuspectId,
            FullName = cs.Suspect.FullName,
            RiskLevel = cs.Suspect.RiskLevel
        });
    }

    public async Task<IEnumerable<CaseSuspectResponse>> GetBySuspectIdAsync(int suspectId)
    {
        var list = await _repository.GetBySuspectIdAsync(suspectId);

        return list.Select(cs => new CaseSuspectResponse
        {
            InvestigationCaseId = cs.InvestigationCaseId,
            SuspectId = cs.SuspectId
        });
    }

    public async Task<bool> RemoveAsync(RemoveSuspectRequest request)
    {
        var removed = await _repository.RemoveAsync(
            request.InvestigationCaseId,
            request.SuspectId);

        if (!removed)
            return false;

        await _activityLogger.LogAsync(
            request.InvestigationCaseId,
            "Case-Suspect",
            $"Suspect {request.SuspectId}",
            "Remove",
            $"Suspect {request.SuspectId} removed from Case {request.InvestigationCaseId}.");

        return true;
    }
}