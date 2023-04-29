using DailyPlanner.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Services.EmailSender;

public static class Bootstrapper
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = Settings.Settings.Load<EmailSenderSettings>("EmailSender", configuration);
        services.AddSingleton(settings!);
        services.AddSingleton<IEmailSender, EmailSender>();
        return services;
    }
}