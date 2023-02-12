using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Api.Configuration;

/// <summary>
/// The API versioning configuration.
/// </summary>
public static class VersioningConfiguration
{
    /// <summary>
    /// Adds the API versioning configuration to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The services collection instance</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}