using ShadowTraceAPI.DTOs.Suspect;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Services;

public class SuspectService : ISuspectService
{
    private readonly ISuspectRepository _repository;

    public SuspectService(ISuspectRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SuspectSummaryResponse>> GetAllAsync()
    {
        var suspects = await _repository.GetAllAsync();

        return suspects.Select(s => new SuspectSummaryResponse
        {
            Id = s.Id,
            FullName = s.FullName,
            PhoneNumber = s.PhoneNumber,
            RiskLevel = s.RiskLevel
        });
    }

    public async Task<SuspectResponse?> GetByIdAsync(int id)
    {
        var suspect = await _repository.GetByIdAsync(id);

        if (suspect == null)
            return null;

        return MapToResponse(suspect);
    }

    public async Task<SuspectResponse> CreateAsync(CreateSuspectRequest request)
    {
        var suspect = new Suspect
        {
            FullName = request.FullName,
            DateOfBirth = DateTime.SpecifyKind(
                request.DateOfBirth,
                DateTimeKind.Utc),
            Gender = request.Gender,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            Notes = request.Notes,
            RiskLevel = request.RiskLevel,
            CreatedAt = DateTime.UtcNow
        };

        suspect = await _repository.AddAsync(suspect);

        return MapToResponse(suspect);
    }

    public async Task<SuspectResponse?> UpdateAsync(
        int id,
        UpdateSuspectRequest request)
    {
        var suspect = await _repository.GetByIdAsync(id);

        if (suspect == null)
            return null;

        suspect.FullName = request.FullName;
        suspect.DateOfBirth = DateTime.SpecifyKind(
            request.DateOfBirth,
            DateTimeKind.Utc);
        suspect.Gender = request.Gender;
        suspect.Address = request.Address;
        suspect.PhoneNumber = request.PhoneNumber;
        suspect.Notes = request.Notes;
        suspect.RiskLevel = request.RiskLevel;

        await _repository.UpdateAsync(suspect);

        return MapToResponse(suspect);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static SuspectResponse MapToResponse(Suspect suspect)
    {
        return new SuspectResponse
        {
            Id = suspect.Id,
            FullName = suspect.FullName,
            DateOfBirth = suspect.DateOfBirth,
            Gender = suspect.Gender,
            Address = suspect.Address,
            PhoneNumber = suspect.PhoneNumber,
            Notes = suspect.Notes,
            RiskLevel = suspect.RiskLevel,
            CreatedAt = suspect.CreatedAt
        };
    }
}