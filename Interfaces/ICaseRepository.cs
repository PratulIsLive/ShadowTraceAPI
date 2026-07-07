using ShadowTraceAPI.Entities;

namespace ShadowTraceAPI.Interfaces;

public interface ICaseRepository
{
    Task<InvestigationCase> CreateAsync(InvestigationCase investigationCase);

    Task<List<InvestigationCase>> GetAllAsync(
        int pageNumber,
        int pageSize,
        string? search,
        string? sortBy);

    Task<InvestigationCase?> GetByIdAsync(int id);

    Task<InvestigationCase?> GetByCaseNumberAsync(string caseNumber);

    Task UpdateAsync(InvestigationCase investigationCase);

    Task DeleteAsync(InvestigationCase investigationCase);
}