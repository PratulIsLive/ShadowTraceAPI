using Microsoft.EntityFrameworkCore;
using ShadowTraceAPI.Data;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Repositories;

public class CaseSuspectRepository : ICaseSuspectRepository
{
    private readonly AppDbContext _context;

    public CaseSuspectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CaseSuspect> AddAsync(CaseSuspect caseSuspect)
    {
        _context.CaseSuspects.Add(caseSuspect);
        await _context.SaveChangesAsync();
        return caseSuspect;
    }

    public async Task<bool> ExistsAsync(int caseId, int suspectId)
    {
        return await _context.CaseSuspects
            .AnyAsync(cs =>
                cs.InvestigationCaseId == caseId &&
                cs.SuspectId == suspectId);
    }

    public async Task<IEnumerable<CaseSuspect>> GetByCaseIdAsync(int caseId)
    {
        return await _context.CaseSuspects
            .Include(cs => cs.Suspect)
            .Where(cs => cs.InvestigationCaseId == caseId)
            .ToListAsync();
    }

    public async Task<IEnumerable<CaseSuspect>> GetBySuspectIdAsync(int suspectId)
    {
        return await _context.CaseSuspects
            .Include(cs => cs.InvestigationCase)
            .Where(cs => cs.SuspectId == suspectId)
            .ToListAsync();
    }

    public async Task<bool> RemoveAsync(int caseId, int suspectId)
    {
        var entity = await _context.CaseSuspects.FindAsync(caseId, suspectId);

        if (entity == null)
            return false;

        _context.CaseSuspects.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> InvestigationCaseExistsAsync(int caseId)
    {
        return await _context.InvestigationCases
            .AnyAsync(c => c.Id == caseId);
    }

    public async Task<bool> SuspectExistsAsync(int suspectId)
    {
        return await _context.Suspects
            .AnyAsync(s => s.Id == suspectId);
    }
}