using DailyPlanner.Web.Pages.Auth.Models;

namespace DailyPlanner.Web.Pages.Auth.Services;

public interface IAuthService
{
    /// <summary>
    /// Attempts to login with the given login model and returns the result.
    /// </summary>
    /// <param name="loginModel">The login model containing login information.</param>
    /// <returns>The result of the login attempt.</returns>
    Task<LoginResult> Login(LoginModel loginModel);

    /// <summary>
    /// Logs out the user by clearing any authentication tokens.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Logout();

    /// <summary>
    /// Refreshes the access token using the refresh token.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RefreshToken();
}