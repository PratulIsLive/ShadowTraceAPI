using ShadowTraceAPI.DTOs.Case;
using ShadowTraceAPI.DTOs.Common;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;
using ShadowTraceAPI.Exceptions;

namespace ShadowTraceAPI.Services;

public class CaseService
{
    private readonly ICaseRepository _caseRepository;
    private readonly ActivityLogger _activityLogger;

    private readonly ICurrentUserService _currentUserService;

    public CaseService(
        ICaseRepository caseRepository,
        ActivityLogger activityLogger,
        ICurrentUserService currentUserService)
    {
        _caseRepository = caseRepository;
        _activityLogger = activityLogger;
        _currentUserService = currentUserService;
    }

    public async Task<CaseResponse> CreateAsync(CreateCaseRequest request)
    {
        var existingCase =
            await _caseRepository.GetByCaseNumberAsync(request.CaseNumber);

        if (existingCase != null)
            throw new ConflictException("Case number already exists.");

        var investigationCase = new InvestigationCase
        {
            CaseNumber = request.CaseNumber,
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            IncidentDate = request.IncidentDate,
            ReporterName = request.ReporterName,
            Priority = request.Priority,

            // Temporary until JWT CurrentUserService is fully integrated
            CreatedById = _currentUserService.UserId
        };

        var createdCase =
            await _caseRepository.CreateAsync(investigationCase);

        // Automatic Activity Log
        await _activityLogger.LogAsync(
            createdCase.Id,
            "Case",
            createdCase.CaseNumber,
            "Create",
            $"Case {createdCase.CaseNumber} was created.");

        return MapToResponse(createdCase);
    }

    public async Task<List<CaseSummaryResponse>> GetAllAsync(
        PaginationRequest request)
    {
        var cases = await _caseRepository.GetAllAsync(
            request.PageNumber,
            request.PageSize,
            request.Search,
            request.SortBy);

        return cases.Select(c => new CaseSummaryResponse
        {
            Id = c.Id,
            CaseNumber = c.CaseNumber,
            Title = c.Title,
            Priority = c.Priority,
            Status = c.Status
        }).ToList();
    }

    public async Task<CaseResponse?> GetByIdAsync(int id)
    {
        var investigationCase =
            await _caseRepository.GetByIdAsync(id);

        if (investigationCase == null)
            return null;

        return MapToResponse(investigationCase);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateCaseRequest request)
    {
        var investigationCase =
            await _caseRepository.GetByIdAsync(id);

        if (investigationCase == null)
            return false;

        investigationCase.Title = request.Title;
        investigationCase.Description = request.Description;
        investigationCase.Location = request.Location;
        investigationCase.IncidentDate = request.IncidentDate;
        investigationCase.ReporterName = request.ReporterName;
        investigationCase.Priority = request.Priority;
        investigationCase.Status = request.Status;
        investigationCase.UpdatedAt = DateTime.UtcNow;

        await _caseRepository.UpdateAsync(investigationCase);

        // Automatic Activity Log
        await _activityLogger.LogAsync(
            investigationCase.Id,
            "Case",
            investigationCase.CaseNumber,
            "Update",
            $"Case {investigationCase.CaseNumber} was updated.");

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var investigationCase =
            await _caseRepository.GetByIdAsync(id);

        if (investigationCase == null)
            return false;

        // Log before deleting
        await _activityLogger.LogAsync(
            investigationCase.Id,
            "Case",
            investigationCase.CaseNumber,
            "Delete",
            $"Case {investigationCase.CaseNumber} was deleted.");

        await _caseRepository.DeleteAsync(investigationCase);

        return true;
    }

    private static CaseResponse MapToResponse(
        InvestigationCase investigationCase)
    {
        return new CaseResponse
        {
            Id = investigationCase.Id,
            CaseNumber = investigationCase.CaseNumber,
            Title = investigationCase.Title,
            Description = investigationCase.Description,
            Location = investigationCase.Location,
            IncidentDate = investigationCase.IncidentDate,
            ReporterName = investigationCase.ReporterName,
            Priority = investigationCase.Priority,
            Status = investigationCase.Status,
            CreatedAt = investigationCase.CreatedAt
        };
    }
}