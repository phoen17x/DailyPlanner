using DailyPlanner.Services.UserAccount.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Services.UserAccount;

public static class Bootstrapper
{
    public static IServiceCollection AddUserAccountService(this IServiceCollection services)
    {
        services.AddScoped<IUserAccountService, UserAccountService>();
        services.AddSingleton<IValidator<RegisterUserAccountModel>, RegisterUserAccountModelValidator>();
        return services;
    }
}