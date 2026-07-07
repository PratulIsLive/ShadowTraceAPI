using System.ComponentModel.DataAnnotations;

namespace ShadowTraceAPI.DTOs.CaseSuspect;

public class RemoveSuspectRequest
{
    [Required(ErrorMessage = "Investigation Case Id is required.")]
    [Range(1, int.MaxValue,
        ErrorMessage = "Investigation Case Id must be greater than 0.")]
    public int InvestigationCaseId { get; set; }


    [Required(ErrorMessage = "Suspect Id is required.")]
    [Range(1, int.MaxValue,
        ErrorMessage = "Suspect Id must be greater than 0.")]
    public int SuspectId { get; set; }
    
}