using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Services.EmailSender;

public static class Bootstrapper
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services)
    {
        services.AddSingleton<IEmailSender, EmailSender>();

        return services;
    }
}