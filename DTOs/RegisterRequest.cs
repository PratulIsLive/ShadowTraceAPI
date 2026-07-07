using System.ComponentModel.DataAnnotations;

namespace ShadowTraceAPI.DTOs;

public class RegisterRequest
{
    [Required(ErrorMessage = "Full Name is required.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Full Name must be between 3 and 100 characters.")]
    public string FullName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
    public string Password { get; set; } = string.Empty;
    
}