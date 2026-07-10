using System.ComponentModel.DataAnnotations;

namespace Aperture.Models;

public class RegistrationModel
{
    [Required]
    [StringLength(64, MinimumLength = 3)]
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Confirm Email Address")]
    [Compare("Email", ErrorMessage = "The email and confirmation email do not match.")]
    public string ConfirmEmail { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 8)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}