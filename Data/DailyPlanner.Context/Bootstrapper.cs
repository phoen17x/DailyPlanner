using DailyPlanner.Context.Factories;
using DailyPlanner.Context.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Context;

/// <summary>
/// Registers database context.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the database context to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the database context to.</param>
    /// <param name="configuration">The optional <see cref="IConfiguration"/> instance to use for configuration settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppDbContext(
        this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        var settings = DailyPlanner.Settings.Settings.Load<DbSettings>("Database", configuration)!;
        services.AddSingleton(settings);

        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(settings.ConnectionString);

        services.AddDbContextFactory<DailyPlannerContext>(dbInitOptionsDelegate);
        return services;
    }
}