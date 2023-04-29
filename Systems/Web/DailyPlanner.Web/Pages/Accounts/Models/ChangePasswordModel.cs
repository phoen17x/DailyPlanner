using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Web.Pages.Accounts.Models;

public class ChangePasswordModel
{
    [Required(ErrorMessage = "Token is required.")]
    public string Token { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(50, ErrorMessage = "Password can not be more than 50 characters long.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(50, ErrorMessage = "Password can not be more than 50 characters long.")]
    [Compare("Password", ErrorMessage = "Passwords must match.")]
    public string ConfirmationPassword { get; set; }
}