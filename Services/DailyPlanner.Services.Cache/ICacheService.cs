namespace DailyPlanner.Services.Cache;

/// <summary>
/// Provides a cache service that can store and retrieve data using keys.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Generates a new unique key.
    /// </summary>
    /// <returns>A unique key.</returns>
    string GenerateKey();

    /// <summary>
    /// Puts a data into the cache with a given key.
    /// </summary>
    /// <typeparam name="T">The type of data.</typeparam>
    /// <param name="key">The key to use.</param>
    /// <param name="data">The data to put.</param>
    /// <param name="lifetime">The lifetime of the data.</param>
    /// <returns>True if the data was put successfully, otherwise false.</returns>
    Task<bool> Put<T>(string key, T data, TimeSpan? lifetime = null);

    /// <summary>
    /// Sets the expiration time of the data with the given key.
    /// </summary>
    /// <param name="key">The key to use.</param>
    /// <param name="lifetime">The lifetime of the data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetExpiration(string key, TimeSpan? lifetime = null);

    /// <summary>
    /// Gets the data with the given key.
    /// </summary>
    /// <typeparam name="T">The type of data.</typeparam>
    /// <param name="key">The key to use.</param>
    /// <param name="resetLifetime">Whether to reset the lifetime of the data or not.</param>
    /// <returns>The data if it exists, otherwise null.</returns>
    Task<T?> Get<T>(string key, bool resetLifetime = false);

    /// <summary>
    /// Deletes the data with the given key.
    /// </summary>
    /// <param name="key">The key to use.</param>
    /// <returns>True if the data was deleted successfully, otherwise false.</returns>
    Task<bool> Delete(string key);
}