using Duende.IdentityServer.Services;

namespace DailyPlanner.Identity.Configuration;

/// <summary>
/// The CORS configuration.
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Adds CORS services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection instance.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddSingleton<ICorsPolicyService>(serviceProvider =>
        {
            var logger = serviceProvider.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
            return new DefaultCorsPolicyService(logger) { AllowAll = true };
        });

        return services;
    }
}
