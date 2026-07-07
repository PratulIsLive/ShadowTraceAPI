using Microsoft.EntityFrameworkCore;
using ShadowTraceAPI.Data;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly AppDbContext _context;

    public ActivityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ActivityLog> CreateAsync(ActivityLog activity)
    {
        _context.ActivityLogs.Add(activity);
        await _context.SaveChangesAsync();

        return activity;
    }

    public async Task<List<ActivityLog>> GetAllAsync()
    {
        return await _context.ActivityLogs
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<ActivityLog?> GetByIdAsync(int id)
    {
        return await _context.ActivityLogs
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<ActivityLog>> GetByCaseIdAsync(int caseId)
    {
        return await _context.ActivityLogs
            .Where(a => a.InvestigationCaseId == caseId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }
}