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
    Task SendConfirmationLinkAsync(string email, string confirmationLink);
    Task SendConfirmationLinkAsync(SendEmailWithLinkModel model);
    Task ConfirmEmail(string token, string email);
    Task<string> GetPasswordRecoveryToken(Guid userId);
    Task SendPasswordRecoveryLinkAsync(SendEmailWithLinkModel model);
    Task ChangePassword(ChangePasswordModel model);
    Task<UserAccountModel> GetUserData(Guid userId);
}