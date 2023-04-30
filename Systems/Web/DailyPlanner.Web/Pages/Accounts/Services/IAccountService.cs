using DailyPlanner.Web.Pages.Accounts.Models;

namespace DailyPlanner.Web.Pages.Accounts.Services;

public interface IAccountService
{
    /// <summary>
    /// Registers a new user with the provided sign-up information.
    /// </summary>
    /// <param name="model">The sign-up information.</param>
    /// <returns>An <see cref="AccountResult"/> object that represents the result of the operation.</returns>
    Task<AccountResult> Register(SignUpModel model);

    /// <summary>
    /// Gets a password recovery token.
    /// </summary>
    /// <returns>The password recovery token.</returns>
    Task<string> GetPasswordRecoveryToken();

    /// <summary>
    /// Sends a password recovery email.
    /// </summary>
    /// <param name="model">The model that contains the email address to send the recovery link to.</param>
    /// <returns>An <see cref="AccountResult"/> object that represents the result of the operation.</returns>
    Task<AccountResult> SendPasswordRecoveryLink(SendEmailWithLinkModel model);

    /// <summary>
    /// Changes the password of the current user.
    /// </summary>
    /// <param name="model">The model that contains the current password and the new password.</param>
    /// <returns>An <see cref="AccountResult"/> object that represents the result of the operation.</returns>
    Task<AccountResult> ChangePassword(ChangePasswordModel model);

    /// <summary>
    /// Retrieves the user data of the currently logged-in user.
    /// </summary>
    /// <returns>The <see cref="UserModel"/> object that represents the user data.</returns>
    Task<UserModel> GetUserData();

    /// <summary>
    /// Confirms the email address of the user.
    /// </summary>
    /// <param name="model">The model that contains the email confirmation token.</param>
    /// <returns>An <see cref="AccountResult"/> object that represents the result of the operation.</returns>
    Task<AccountResult> ConfirmEmail(ConfirmEmailModel model);

    /// <summary>
    /// Sends a confirmation email.
    /// </summary>
    /// <param name="model">The model that contains the email address to send the confirmation link to.</param>
    /// <returns>An <see cref="AccountResult"/> object that represents the result of the operation.</returns>
    Task<AccountResult> SendConfirmationEmail(SendEmailWithLinkModel model);
}