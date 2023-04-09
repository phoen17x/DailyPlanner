namespace DailyPlanner.Services.Cache;

/// <summary>
/// Represents cache settings.
/// </summary>
public class CacheSettings
{
    /// <summary>
    /// Gets or sets cache lifetime in minutes.
    /// </summary>
    public int Lifetime { get; private set; }

    /// <summary>
    /// Gets or sets the URI of the cache server.
    /// </summary>
    public string? Uri { get; private set; }
}