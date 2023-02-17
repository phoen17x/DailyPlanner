using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Context.Setup;

/// <summary>
/// Initializes the database by executing pending migrations. 
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Initializes the database by applying any pending migrations.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use for obtaining a database context.</param>
    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DailyPlannerContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}