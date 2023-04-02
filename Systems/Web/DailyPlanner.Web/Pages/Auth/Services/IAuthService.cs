using DailyPlanner.Web.Pages.Auth.Models;
using Microsoft.AspNetCore.Http;

namespace DailyPlanner.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
    Task RefreshToken();
}