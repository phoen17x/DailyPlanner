using Microsoft.Extensions.Configuration;

namespace DailyPlanner.Settings;

/// <summary>
/// Provides the functionality to create instances of <see cref="IConfiguration"/>.
/// </summary>
public static class SettingsFactory
{
    /// <summary>
    /// Creates an instance of <see cref="IConfiguration"/>.
    /// </summary>
    /// <param name="configuration">The existing instance of <see cref="IConfiguration"/>.</param>
    /// <returns>An instance of <see cref="IConfiguration"/>.</returns>
    public static IConfiguration Create(IConfiguration? configuration = null)
    {
        return configuration ?? new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile("appsettings.development.json", true)
            .AddEnvironmentVariables()
            .Build();
    }
}