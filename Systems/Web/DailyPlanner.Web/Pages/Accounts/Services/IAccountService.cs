using DailyPlanner.Web.Pages.Accounts.Models;

namespace DailyPlanner.Web.Pages.Accounts.Services;

public interface IAccountService
{
    Task<SignUpResult> Register(SignUpModel model);
}