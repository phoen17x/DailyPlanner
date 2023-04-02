using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Web.Pages.Accounts.Models;

public class SignUpModel
{
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    [MaxLength(16, ErrorMessage = "Name can not be more than 16 characters long.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(50, ErrorMessage = "Password can not be more than 50 characters long.")]
    public string Password { get; set; }
}