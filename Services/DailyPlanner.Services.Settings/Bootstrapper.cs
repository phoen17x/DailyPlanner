namespace DailyPlanner.Services.Settings;

using DailyPlanner.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Adds settings to the specified <see cref="IServiceCollection"/>.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds Swagger settings to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection instance.</param>
    /// <param name="configuration">Optional configuration to load settings from.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = Settings.Load<SwaggerSettings>("Swagger", configuration);
        services.AddSingleton(settings!);

        return services;
    }

    /// <summary>
    /// Adds identity settings to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection instance.</param>
    /// <param name="configuration">Optional configuration to load settings from.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddIdentitySettings(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = Settings.Load<IdentitySettings>("Identity", configuration);
        services.AddSingleton(settings!);

        return services;
    }
}