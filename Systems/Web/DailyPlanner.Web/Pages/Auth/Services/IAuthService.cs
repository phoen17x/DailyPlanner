using DailyPlanner.Web.Pages.Auth.Models;

namespace DailyPlanner.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}