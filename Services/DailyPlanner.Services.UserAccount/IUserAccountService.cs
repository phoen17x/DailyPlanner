using DailyPlanner.Services.UserAccount.Models;

namespace DailyPlanner.Services.UserAccount;

public interface IUserAccountService
{
    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="model">Data for the new user.</param>
    /// <returns>The newly created <see cref="UserAccountModel"/> object.</returns>
    Task<UserAccountModel> AddUserAccount(RegisterUserAccountModel model);

    /// <summary>
    /// Sends an email with a confirmation link to the specified email address.
    /// </summary>
    /// <param name="email">The email address to send the confirmation link to.</param>
    /// <param name="confirmationLink">The confirmation link to send.</param>
    Task SendConfirmationLinkAsync(string email, string confirmationLink);

    /// <summary>
    /// Sends an email with a confirmation link to the specified email address.
    /// </summary>
    /// <param name="model">The data for sending the email with the confirmation link.</param>
    Task SendConfirmationLinkAsync(SendEmailWithLinkModel model);

    /// <summary>
    /// Confirms the email address associated with the specified token and email address.
    /// </summary>
    /// <param name="token">The confirmation token to use.</param>
    /// <param name="email">The email address associated with the confirmation token.</param>
    Task ConfirmEmail(string token, string email);

    /// <summary>
    /// Generates a password recovery token for the user with the specified ID.
    /// </summary>
    /// <param name="userId">The ID of the user to generate a password recovery token for.</param>
    /// <returns>The generated password recovery token.</returns>
    Task<string> GetPasswordRecoveryToken(Guid userId);

    /// <summary>
    /// Sends an email with a password recovery link to the specified email address.
    /// </summary>
    /// <param name="model">The data for sending the email with the password recovery link.</param>
    Task SendPasswordRecoveryLinkAsync(SendEmailWithLinkModel model);

    /// <summary>
    /// Changes user password.
    /// </summary>
    /// <param name="model">The data for changing the password.</param>
    Task ChangePassword(ChangePasswordModel model);

    /// <summary>
    /// Retrieves the user account data for the user with the specified ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve data for.</param>
    /// <returns>The <see cref="UserAccountModel"/> object for the specified user.</returns>
    Task<UserAccountModel> GetUserData(Guid userId);
}