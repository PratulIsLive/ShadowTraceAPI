using Microsoft.EntityFrameworkCore;
using ShadowTraceAPI.Data;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Repositories;

public class CaseRepository : ICaseRepository
{
    private readonly AppDbContext _context;

    public CaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<InvestigationCase> CreateAsync(InvestigationCase investigationCase)
    {
        _context.InvestigationCases.Add(investigationCase);
        await _context.SaveChangesAsync();

        return investigationCase;
    }

    public async Task<List<InvestigationCase>> GetAllAsync(
        int pageNumber,
        int pageSize,
        string? search,
        string? sortBy)
    {
        var query = _context.InvestigationCases.AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();

            query = query.Where(c =>
                c.CaseNumber.ToLower().Contains(search) ||
                c.Title.ToLower().Contains(search) ||
                c.Location.ToLower().Contains(search) ||
                c.ReporterName.ToLower().Contains(search));
        }

        // Sorting
        query = sortBy?.ToLower() switch
        {
            "priority" => query.OrderBy(c => c.Priority),

            "status" => query.OrderBy(c => c.Status),

            "title" => query.OrderBy(c => c.Title),

            "date" => query.OrderByDescending(c => c.IncidentDate),

            _ => query.OrderByDescending(c => c.CreatedAt)
        };

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<InvestigationCase?> GetByIdAsync(int id)
    {
        return await _context.InvestigationCases
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<InvestigationCase?> GetByCaseNumberAsync(string caseNumber)
    {
        return await _context.InvestigationCases
            .FirstOrDefaultAsync(c => c.CaseNumber == caseNumber);
    }

    public async Task UpdateAsync(InvestigationCase investigationCase)
    {
        _context.InvestigationCases.Update(investigationCase);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(InvestigationCase investigationCase)
    {
        _context.InvestigationCases.Remove(investigationCase);
        await _context.SaveChangesAsync();
    }
}