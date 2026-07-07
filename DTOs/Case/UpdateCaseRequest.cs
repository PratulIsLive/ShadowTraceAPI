using System.ComponentModel.DataAnnotations;
using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Case;

public class UpdateCaseRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(150,
        MinimumLength = 5,
        ErrorMessage = "Title must be between 5 and 150 characters.")]
    public string Title { get; set; } = string.Empty;



    [StringLength(1000,
        ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; } = string.Empty;



    [Required(ErrorMessage = "Location is required.")]
    [StringLength(200,
        ErrorMessage = "Location cannot exceed 200 characters.")]
    public string Location { get; set; } = string.Empty;



    [Required(ErrorMessage = "Incident Date is required.")]
    public DateTime IncidentDate { get; set; }



    [Required(ErrorMessage = "Reporter Name is required.")]
    [StringLength(100,
        MinimumLength = 3,
        ErrorMessage = "Reporter Name must be between 3 and 100 characters.")]
    public string ReporterName { get; set; } = string.Empty;



    [Required(ErrorMessage = "Priority is required.")]
    public PriorityLevel Priority { get; set; }



    [Required(ErrorMessage = "Status is required.")]
    public CaseStatus Status { get; set; }
    
}