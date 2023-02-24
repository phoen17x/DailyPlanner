namespace DailyPlanner.Api.Configuration;

/// <summary>
/// The AutoMapper configuration.
/// </summary>
public static class AutoMapperConfiguration
{
    /// <summary>
    /// Adds AutoMapper to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add AutoMapper to.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => assembly.FullName != null && assembly.FullName.ToLower().StartsWith("dailyplanner"));

        services.AddAutoMapper(assemblies);

        return services;
    }
}