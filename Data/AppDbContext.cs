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

        // ============================
        // Composite Key
        // ============================

        modelBuilder.Entity<CaseSuspect>()
            .HasKey(cs => new { cs.InvestigationCaseId, cs.SuspectId });

        // ============================
        // User -> CaseAssignment
        // ============================

        modelBuilder.Entity<CaseAssignment>()
            .HasOne(ca => ca.Investigator)
            .WithMany(u => u.CaseAssignments)
            .HasForeignKey(ca => ca.InvestigatorId)
            .OnDelete(DeleteBehavior.Restrict);

        // ============================
        // InvestigationCase -> CaseAssignment
        // ============================

        modelBuilder.Entity<CaseAssignment>()
            .HasOne(ca => ca.InvestigationCase)
            .WithMany(c => c.CaseAssignments)
            .HasForeignKey(ca => ca.InvestigationCaseId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================
        // InvestigationCase -> Evidence
        // ============================

        modelBuilder.Entity<Evidence>()
            .HasOne(e => e.InvestigationCase)
            .WithMany(c => c.Evidence)
            .HasForeignKey(e => e.InvestigationCaseId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================
        // User -> Evidence
        // ============================

        modelBuilder.Entity<Evidence>()
            .HasOne(e => e.UploadedBy)
            .WithMany(u => u.UploadedEvidence)
            .HasForeignKey(e => e.UploadedById)
            .OnDelete(DeleteBehavior.Restrict);

        // ============================
        // InvestigationCase -> ActivityLog
        // ============================

        modelBuilder.Entity<ActivityLog>()
            .HasOne(a => a.InvestigationCase)
            .WithMany(c => c.ActivityLogs)
            .HasForeignKey(a => a.InvestigationCaseId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================
        // User -> ActivityLog
        // ============================

        modelBuilder.Entity<ActivityLog>()
            .HasOne(a => a.PerformedBy)
            .WithMany(u => u.ActivityLogs)
            .HasForeignKey(a => a.PerformedById)
            .OnDelete(DeleteBehavior.Restrict);

        // ============================
        // InvestigationCase -> CaseSuspect
        // ============================

        modelBuilder.Entity<CaseSuspect>()
            .HasOne(cs => cs.InvestigationCase)
            .WithMany(c => c.CaseSuspects)
            .HasForeignKey(cs => cs.InvestigationCaseId);

        // ============================
        // Suspect -> CaseSuspect
        // ============================

        modelBuilder.Entity<CaseSuspect>()
            .HasOne(cs => cs.Suspect)
            .WithMany(s => s.CaseSuspects)
            .HasForeignKey(cs => cs.SuspectId);

        // ============================
        // Useful Indexes
        // ============================

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<InvestigationCase>()
            .HasIndex(c => c.CaseNumber)
            .IsUnique();

        modelBuilder.Entity<Suspect>()
            .HasIndex(s => s.PhoneNumber);

        modelBuilder.Entity<Evidence>()
            .HasIndex(e => e.EvidenceNumber);
    }
}