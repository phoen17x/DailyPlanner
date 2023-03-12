using Duende.IdentityServer.Models;

namespace DailyPlanner.Identity.Configuration.IdentityServer;

/// <summary>
/// Provides a collection of identity resources.
/// </summary>
public static class AppIdentityResources
{
    /// <summary>
    /// Gets a collection of identity resources.
    /// </summary>
    public static IEnumerable<IdentityResource> Resources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };
}