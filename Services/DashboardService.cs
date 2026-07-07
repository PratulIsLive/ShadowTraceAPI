using Microsoft.EntityFrameworkCore;
using ShadowTraceAPI.Data;
using ShadowTraceAPI.DTOs.Dashboard;
using ShadowTraceAPI.Enums;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Services;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardResponse> GetDashboardAsync()
    {
        var response = new DashboardResponse
        {
            TotalCases = await _context.InvestigationCases.CountAsync(),

            OpenCases = await _context.InvestigationCases
                .CountAsync(c => c.Status == CaseStatus.Open),

            ClosedCases = await _context.InvestigationCases
                .CountAsync(c => c.Status == CaseStatus.Closed),

            TotalSuspects = await _context.Suspects.CountAsync(),

            TotalEvidence = await _context.Evidence.CountAsync()
        };

        response.RecentCases = await _context.InvestigationCases
            .OrderByDescending(c => c.CreatedAt)
            .Take(5)
            .Select(c => new RecentCaseResponse
            {
                Id = c.Id,
                CaseNumber = c.CaseNumber,
                Title = c.Title,
                Priority = c.Priority,
                Status = c.Status,
                CreatedAt = c.CreatedAt
            })

            .ToListAsync();

        return response;
    }
}