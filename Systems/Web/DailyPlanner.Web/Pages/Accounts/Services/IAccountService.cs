using DailyPlanner.Web.Pages.Accounts.Models;

namespace DailyPlanner.Web.Pages.Accounts.Services;

public interface IAccountService
{
    Task<AccountResult> Register(SignUpModel model);
    Task<string> GetPasswordRecoveryToken();
    Task<AccountResult> SendPasswordRecoveryLink(SendEmailWithLinkModel model);
    Task<AccountResult> ChangePassword(ChangePasswordModel model);
    Task<UserModel> GetUserData();
    Task<AccountResult> ConfirmEmail(ConfirmEmailModel model);
    Task<AccountResult> SendConfirmationEmail(SendEmailWithLinkModel model);
}