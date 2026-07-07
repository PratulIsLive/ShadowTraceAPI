using System.ComponentModel.DataAnnotations;
using ShadowTraceAPI.Enums;

namespace ShadowTraceAPI.DTOs.Suspect;

public class UpdateSuspectRequest
{
    [Required(ErrorMessage = "Full Name is required.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Full Name must be between 3 and 100 characters.")]
    public string FullName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Date of Birth is required.")]
    public DateTime DateOfBirth { get; set; }


    [Required(ErrorMessage = "Gender is required.")]
    [RegularExpression("^(Male|Female|Other)$",
        ErrorMessage = "Gender must be Male, Female or Other.")]
    public string Gender { get; set; } = string.Empty;


    [Required(ErrorMessage = "Address is required.")]
    [StringLength(250,
        ErrorMessage = "Address cannot exceed 250 characters.")]
    public string Address { get; set; } = string.Empty;


    [Required(ErrorMessage = "Phone Number is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    [StringLength(15,
        ErrorMessage = "Phone Number cannot exceed 15 characters.")]
    public string PhoneNumber { get; set; } = string.Empty;


    [StringLength(1000,
        ErrorMessage = "Notes cannot exceed 1000 characters.")]
    public string Notes { get; set; } = string.Empty;


    [Required(ErrorMessage = "Risk Level is required.")]
    public RiskLevel RiskLevel { get; set; }
    
}