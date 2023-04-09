using DailyPlanner.Common.Extensions;
using StackExchange.Redis;

namespace DailyPlanner.Services.Cache;

/// <summary>
/// Provides an implementation of <see cref="ICacheService"/>.
/// </summary>
public class CacheService : ICacheService
{
    /// <summary>
    /// The default lifetime of a cache entry.
    /// </summary>
    private TimeSpan defaultLifetime;

    /// <summary>
    /// The configuration settings for the cache service.
    /// </summary>
    private readonly CacheSettings settings;

    /// <summary>
    /// The Redis database used for caching data.
    /// </summary>
    private readonly IDatabase cacheDatabase;

    /// <summary>
    /// The URI of the Redis server.
    /// </summary>
    private static string? redisUri;

    /// <summary>
    /// A lazily-initialized connection to the Redis server.
    /// </summary>
    private static Lazy<ConnectionMultiplexer> lazyConnection = new(() => ConnectionMultiplexer.Connect(redisUri));

    /// <summary>
    /// Gets the connection to the Redis server.
    /// </summary>
    private static ConnectionMultiplexer Connection => lazyConnection.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheService"/> class.
    /// </summary>
    /// <param name="settings">The configuration settings for the cache service.</param>
    public CacheService(CacheSettings settings)
    {
        this.settings = settings;
        redisUri = this.settings.Uri;
        defaultLifetime = TimeSpan.FromMinutes(this.settings.Lifetime);
        cacheDatabase = Connection.GetDatabase();
    }

    public string GenerateKey()
    {
        return Guid.NewGuid().Shrink();
    }

    public async Task<bool> Put<T>(string key, T data, TimeSpan? lifetime = null)
    {
        return await cacheDatabase.StringSetAsync(key, data?.ToJsonString(), lifetime ?? defaultLifetime);
    }

    public async Task SetExpiration(string key, TimeSpan? lifetime = null)
    {
        await cacheDatabase.KeyExpireAsync(key, lifetime ?? defaultLifetime);
    }

    public async Task<T?> Get<T>(string key, bool resetLifetime = false)
    {
        string? cachedData = await cacheDatabase.StringGetAsync(key);
        if (string.IsNullOrEmpty(cachedData)) return default;

        var data = cachedData.FromJsonString<T>();
        if (resetLifetime) await SetExpiration(key);

        return data;
    }

    public async Task<bool> Delete(string key)
    {
        return await cacheDatabase.KeyDeleteAsync(key);
    }
}