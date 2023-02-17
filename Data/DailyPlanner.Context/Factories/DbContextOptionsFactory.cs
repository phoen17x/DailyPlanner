using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Context.Factories;

/// <summary>
/// Factory for creating <see cref="DbContextOptions"/> for <see cref="DailyPlannerContext"/>.
/// </summary>
public class DbContextOptionsFactory
{
    private const string migrationsAssembly = "DailyPlanner.Context.MigrationsPostgreSQL";

    /// <summary>
    /// Creates a new <see cref="DbContextOptions"/> for <see cref="DailyPlannerContext"/> with the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <returns>A new <see cref="DbContextOptions"/>.</returns>
    public static DbContextOptions<DailyPlannerContext> Create(string connectionString)
    {
        var builder = new DbContextOptionsBuilder<DailyPlannerContext>();
        Configure(connectionString).Invoke(builder);
        return builder.Options;
    }

    /// <summary>
    /// Configures the <see cref="DbContextOptionsBuilder"/> with the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <returns>An action that configures the <see cref="DbContextOptionsBuilder"/>.</returns>
    public static Action<DbContextOptionsBuilder> Configure(string connectionString)
    {
        return builder =>
        {
            builder.UseNpgsql(connectionString, options =>
            {
                options
                    .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                    .MigrationsHistoryTable("__EFMigrationsHistory", "public")
                    .MigrationsAssembly(migrationsAssembly);
            });

            builder
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };
    }
}