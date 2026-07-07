using Microsoft.EntityFrameworkCore;
using ShadowTraceAPI.Data;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Repositories;

public class SuspectRepository : ISuspectRepository
{
    private readonly AppDbContext _context;

    public SuspectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Suspect>> GetAllAsync()
    {
        return await _context.Suspects
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task<Suspect?> GetByIdAsync(int id)
    {
        return await _context.Suspects
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Suspect> AddAsync(Suspect suspect)
    {
        _context.Suspects.Add(suspect);
        await _context.SaveChangesAsync();

        return suspect;
    }

    public async Task<Suspect?> UpdateAsync(Suspect suspect)
    {
        var existing = await _context.Suspects.FindAsync(suspect.Id);

        if (existing == null)
            return null;

        _context.Entry(existing).CurrentValues.SetValues(suspect);

        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var suspect = await _context.Suspects.FindAsync(id);

        if (suspect == null)
            return false;

        _context.Suspects.Remove(suspect);

        await _context.SaveChangesAsync();

        return true;
    }
}