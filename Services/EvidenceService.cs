using ShadowTraceAPI.DTOs.Evidence;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShadowTraceAPI.Exceptions;

namespace ShadowTraceAPI.Services;

public class EvidenceService : IEvidenceService
{
    private readonly IEvidenceRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    private readonly IWebHostEnvironment _environment;

    private readonly ActivityLogger _activityLogger;

    public EvidenceService(
        IEvidenceRepository repository, 
        ICurrentUserService currentUserService,
        IWebHostEnvironment environment,
        ActivityLogger activityLogger)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _environment = environment;
        _activityLogger = activityLogger;
    }

    public async Task<EvidenceResponse> CreateAsync(CreateEvidenceRequest request)
    {
        if (!await _repository.InvestigationCaseExistsAsync(request.InvestigationCaseId))
            throw new NotFoundException("Investigation Case not found.");

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "evidence");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        var evidence = new Evidence
        {
            InvestigationCaseId = request.InvestigationCaseId,
            Title = request.Title,
            Description = request.Description,
            EvidenceType = request.EvidenceType,
            FileName = uniqueFileName,
            StoragePath = $"/evidence/{uniqueFileName}",
            UploadedById = _currentUserService.UserId, 
            UploadedAt = DateTime.UtcNow
        };
            

        evidence = await _repository.CreateAsync(evidence);

        await _activityLogger.LogAsync(
            evidence.InvestigationCaseId,
            "Evidence",
            evidence.Title,
            "Create",
            $"Evidence {evidence.Title} was uploaded.");

        return MapToResponse(evidence);
    }

    public async Task<List<EvidenceSummaryResponse>> GetAllAsync()
    {
        var evidence = await _repository.GetAllAsync();

        return evidence.Select(e => new EvidenceSummaryResponse
        {
            Id = e.Id,
            Title = e.Title,
            EvidenceType = e.EvidenceType,
            UploadedAt = e.UploadedAt
        }).ToList();
    }

    public async Task<EvidenceResponse?> GetByIdAsync(int id)
    {
        var evidence = await _repository.GetByIdAsync(id);

        if (evidence == null)
            return null;

        return MapToResponse(evidence);
    }

    public async Task<EvidenceResponse?> UpdateAsync(
    int id,
    UpdateEvidenceRequest request)
    {
        var evidence = await _repository.GetByIdAsync(id);

        if (evidence == null)
            return null;

        evidence.Title = request.Title;
        evidence.Description = request.Description;
        evidence.EvidenceType = request.EvidenceType;

        if (request.File != null)
        {

            // Delete old file
            if (!string.IsNullOrWhiteSpace(evidence.StoragePath))
            {

                var oldFilePath = Path.Combine(
                _environment.WebRootPath,
                evidence.StoragePath.TrimStart('/')
                    .Replace('/', Path.DirectorySeparatorChar));

                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
           }

            // Ensure uploads folder exists
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "evidence");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Save new file
            var uniqueFileName =
            $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";

            var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(newFilePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            evidence.FileName = uniqueFileName;
            evidence.StoragePath = $"/evidence/{uniqueFileName}";
        }

        await _repository.UpdateAsync(evidence);

        await _activityLogger.LogAsync(
        evidence.InvestigationCaseId,
        "Evidence",
        evidence.Title,
        "Update",
        $"Evidence '{evidence.Title}' was updated.");

        return MapToResponse(evidence);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var evidence = await _repository.GetByIdAsync(id);

        if (evidence == null)
            return false;

        // Delete physical file
        if (!string.IsNullOrWhiteSpace(evidence.StoragePath))
        {
            var filePath = Path.Combine(
            _environment.WebRootPath,
            evidence.StoragePath.TrimStart('/')
                .Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await _repository.DeleteAsync(evidence);

        await _activityLogger.LogAsync(
        evidence.InvestigationCaseId,
        "Evidence",
        evidence.Title,
        "Delete",
        $"Evidence '{evidence.Title}' was deleted.");

        return true;
    }

    public async Task<List<EvidenceSummaryResponse>> GetByCaseIdAsync(int caseId)
    {
        var evidence = await _repository.GetByCaseIdAsync(caseId);

        return evidence.Select(e => new EvidenceSummaryResponse
        {
            Id = e.Id,
            Title = e.Title,
            EvidenceType = e.EvidenceType,
            UploadedAt = e.UploadedAt
        }).ToList();
    }

    private static EvidenceResponse MapToResponse(Evidence evidence)
    {
        return new EvidenceResponse
        {
            Id = evidence.Id,
            InvestigationCaseId = evidence.InvestigationCaseId,
            Title = evidence.Title,
            Description = evidence.Description,
            EvidenceType = evidence.EvidenceType,
            FileName = evidence.FileName,
            StoragePath = evidence.StoragePath,
            UploadedById = evidence.UploadedById,
            UploadedAt = evidence.UploadedAt
        };
    }
}