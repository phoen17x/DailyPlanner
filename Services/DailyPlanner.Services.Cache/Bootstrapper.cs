using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Services.Cache;

public static class Bootstrapper
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var settings = Settings.Settings.Load<CacheSettings>("Cache", configuration);
        services.AddSingleton(settings!);
        services.AddSingleton<ICacheService, CacheService>();
        return services;
    }
}