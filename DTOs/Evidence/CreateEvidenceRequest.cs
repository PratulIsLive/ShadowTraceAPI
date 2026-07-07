using Microsoft.AspNetCore.Http;
using ShadowTraceAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace ShadowTraceAPI.DTOs.Evidence;

public class CreateEvidenceRequest
{
    [Required(ErrorMessage = "Investigation Case Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Investigation Case Id.")]
    public int InvestigationCaseId { get; set; }



    [Required(ErrorMessage = "Evidence title is required.")]
    [StringLength(150,
        MinimumLength = 3,
        ErrorMessage = "Title must be between 3 and 150 characters.")]
    public string Title { get; set; } = string.Empty;



    [StringLength(1000,
        ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; } = string.Empty;



    [Required(ErrorMessage = "Evidence type is required.")]
    [EnumDataType(typeof(EvidenceType),
        ErrorMessage = "Invalid Evidence Type.")]
    public EvidenceType EvidenceType { get; set; }



    [Required(ErrorMessage = "Please upload an evidence file.")]
    public IFormFile File { get; set; } = null!;
    
}