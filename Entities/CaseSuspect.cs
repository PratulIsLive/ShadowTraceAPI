namespace ShadowTraceAPI.Entities;

public class CaseSuspect
{
    public int InvestigationCaseId { get; set; }

    public InvestigationCase InvestigationCase { get; set; } = null!;

    public int SuspectId { get; set; }

    public Suspect Suspect { get; set; } = null!;
}