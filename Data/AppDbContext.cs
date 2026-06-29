using Microsoft.EntityFrameworkCore;
using ShadowTraceAPI.Entities;

namespace ShadowTraceAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<InvestigationCase> InvestigationCases => Set<InvestigationCase>();

    public DbSet<Suspect> Suspects => Set<Suspect>();

    public DbSet<CaseAssignment> CaseAssignments => Set<CaseAssignment>();

    public DbSet<Evidence> Evidence => Set<Evidence>();

    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

    public DbSet<CaseSuspect> CaseSuspects => Set<CaseSuspect>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite Primary Key
        modelBuilder.Entity<CaseSuspect>()
            .HasKey(cs => new { cs.InvestigationCaseId, cs.SuspectId });
    }
}