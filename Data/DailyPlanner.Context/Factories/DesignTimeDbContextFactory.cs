using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DailyPlanner.Context.Factories;

/// <summary>
/// Factory for creating instances of <see cref="DailyPlannerContext"/> for design time.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DailyPlannerContext>
{
    private const string databaseProvider = "postgresql";
    private const string migrationsAssembly = "DailyPlanner.Context.MigrationsPostgreSQL";

    public DailyPlannerContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.context.json")
            .Build();

        var options = new DbContextOptionsBuilder<DailyPlannerContext>()
            .UseNpgsql(
                configuration.GetConnectionString(databaseProvider),
                npgsqlOptions => npgsqlOptions.MigrationsAssembly(migrationsAssembly))
            .Options;

        return new DbContextFactory(options).Create();
    }
}