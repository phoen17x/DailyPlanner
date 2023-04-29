namespace DailyPlanner.Api.Configuration;

/// <summary>
/// The CORS configuration.
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Adds CORS services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The services collection instance</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.SetIsOriginAllowed(host =>true);
                policy.AllowCredentials();
            });
        });
        return services;
    }

    /// <summary>
    /// Uses CORS in the application.
    /// </summary>
    /// <param name="app">The application builder instance</param>
    public static void UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors();
    }
}