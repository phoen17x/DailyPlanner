using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Context.Factories;

/// <summary>
/// Factory for creating instances of <see cref="DailyPlannerContext"/>.
/// </summary>
public class DbContextFactory
{
    /// <summary>
    /// The options to be used to configure the <see cref="DailyPlannerContext"/>.
    /// </summary>
    private readonly DbContextOptions<DailyPlannerContext> options;

    public DbContextFactory(DbContextOptions<DailyPlannerContext> options)
    {
        this.options = options;
    }

    /// <summary>
    /// Creates a new instance of the  <see cref="DailyPlannerContext"/> using the options provided to the factory.
    /// </summary>
    /// <returns>A new instance of  <see cref="DailyPlannerContext"/>.</returns>
    public DailyPlannerContext Create() => new DailyPlannerContext(options);
}