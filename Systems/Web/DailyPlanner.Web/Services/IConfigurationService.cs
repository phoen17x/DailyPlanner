namespace DailyPlanner.Web.Services;

public interface IConfigurationService
{
    /// <summary>
    /// Gets the current value of the dark mode configuration setting.
    /// </summary>
    /// <returns>A boolean indicating whether dark mode is enabled.</returns>
    Task<bool> GetDarkMode();

    /// <summary>
    /// Sets the value of the dark mode configuration setting.
    /// </summary>
    /// <param name="value">The new value for the dark mode configuration setting.</param>
    /// <returns>A task representing the completion of the set operation.</returns>
    Task SetDarkMode(bool value);
}