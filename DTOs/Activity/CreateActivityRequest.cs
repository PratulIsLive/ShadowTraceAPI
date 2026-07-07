using System.ComponentModel.DataAnnotations;

namespace ShadowTraceAPI.DTOs.Activity;

public class CreateActivityRequest
{
    [Required(ErrorMessage = "Investigation Case Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Investigation Case Id must be greater than 0.")]
    public int InvestigationCaseId { get; set; }


    [Required(ErrorMessage = "Module is required.")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Module must be between 2 and 50 characters.")]
    public string Module { get; set; } = string.Empty;


    [Required(ErrorMessage = "Entity Name is required.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Entity Name must be between 2 and 100 characters.")]
    public string EntityName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Action Type is required.")]
    [StringLength(30, MinimumLength = 2,
        ErrorMessage = "Action Type must be between 2 and 30 characters.")]
    public string ActionType { get; set; } = string.Empty;


    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, MinimumLength = 5,
        ErrorMessage = "Description must be between 5 and 500 characters.")]
    public string Description { get; set; } = string.Empty;
    
}