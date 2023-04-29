using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Web.Pages.Accounts.Models;

public class SendEmailWithLinkModel
{
    [Required(ErrorMessage = "Url is required.")]
    public string FrontendUrl { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    public string Email { get; set; }
}