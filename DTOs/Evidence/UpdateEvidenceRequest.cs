using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Evidence;

public class UpdateEvidenceRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(150, MinimumLength = 3,
        ErrorMessage = "Title must be between 3 and 150 characters.")]
    public string Title { get; set; } = string.Empty;
    

    [StringLength(1000,
        ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; } = string.Empty;


    [Required(ErrorMessage = "Evidence Type is required.")]
    public EvidenceType EvidenceType { get; set; }


    // Upload a new file if the evidence needs to be replaced
    public IFormFile? File { get; set; }

}