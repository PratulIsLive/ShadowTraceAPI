using Microsoft.EntityFrameworkCore;
using ShadowTraceAPI.Data;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Repositories;

public class EvidenceRepository : IEvidenceRepository
{
    private readonly AppDbContext _context;

    public EvidenceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Evidence> CreateAsync(Evidence evidence)
    {
        _context.Evidence.Add(evidence);
        await _context.SaveChangesAsync();
        return evidence;
    }

    public async Task<List<Evidence>> GetAllAsync()
    {
        return await _context.Evidence
            .OrderByDescending(e => e.UploadedAt)
            .ToListAsync();
    }

    public async Task<Evidence?> GetByIdAsync(int id)
    {
        return await _context.Evidence
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Evidence>> GetByCaseIdAsync(int caseId)
    {
        return await _context.Evidence
            .Where(e => e.InvestigationCaseId == caseId)
            .OrderByDescending(e => e.UploadedAt)
            .ToListAsync();
    }

    public async Task UpdateAsync(Evidence evidence)
    {
        _context.Evidence.Update(evidence);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Evidence evidence)
    {
        _context.Evidence.Remove(evidence);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> InvestigationCaseExistsAsync(int caseId)
    {
        return await _context.InvestigationCases
            .AnyAsync(c => c.Id == caseId);
    }
}