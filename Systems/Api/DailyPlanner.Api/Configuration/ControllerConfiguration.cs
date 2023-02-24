using DailyPlanner.Common.Extensions;

namespace DailyPlanner.Api.Configuration;

/// <summary>
/// Configures the MVC controllers.
/// </summary>
public static class ControllerConfiguration
{
    /// <summary>
    /// Adds services for controllers to the specified <see cref="IServiceCollection"/> and configures them with default settings for JSON serialization.
    /// </summary>
    /// <param name="services">The services collection instance.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppControllers(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultSettings())
            .AddAppValidator();

        return services;
    }
}