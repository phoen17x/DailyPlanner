using Duende.IdentityServer.Models;

namespace DailyPlanner.Identity.Configuration.IdentityServer;

/// <summary>
/// Provides a collection of API resources.
/// </summary>
public static class AppResources
{
    /// <summary>
    /// Gets a collection of API resources.
    /// </summary>
    public static IEnumerable<ApiResource> Resources => new List<ApiResource>
    {
        new ApiResource("api")
    };
}