using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Web.Pages.Accounts.Models;

public class ConfirmEmailModel
{
    [Required(ErrorMessage = "Token is required.")]
    public string Token { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    public string Email { get; set; }
}
