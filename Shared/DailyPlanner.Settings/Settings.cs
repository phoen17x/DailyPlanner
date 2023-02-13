using Microsoft.Extensions.Configuration;

namespace DailyPlanner.Settings;

/// <summary>
/// Provides a generic method for loading application settings from a configuration source.
/// </summary>
public abstract class Settings
{
    /// <summary>
    /// Loads the application settings of type `T` from a configuration source, specified by the `key`.
    /// </summary>
    /// <typeparam name="T">The type of the application settings to load.</typeparam>
    /// <param name="key">The key of the configuration section to load the settings from.</param>
    /// <param name="configuration">The optional configuration source to use.
    /// If not specified, a default configuration builder will be used.</param>
    /// <returns>An instance of the loaded application settings of type `T`.</returns>
    public static T? Load<T>(string key, IConfiguration? configuration = null)
    {
        var settings = (T?)Activator.CreateInstance(typeof(T));

        SettingsFactory
            .Create(configuration)
            .GetSection(key)
            .Bind(settings, options => options.BindNonPublicProperties = true);

        return settings;
    }
}