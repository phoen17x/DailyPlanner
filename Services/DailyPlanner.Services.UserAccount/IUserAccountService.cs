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
}