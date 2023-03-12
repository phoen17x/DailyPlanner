using DailyPlanner.Common.Security;
using Duende.IdentityServer.Models;

namespace DailyPlanner.Identity.Configuration.IdentityServer;

/// <summary>
/// Provides a collection of API scopes.
/// </summary>
public static class AppApiScopes
{
    /// <summary>
    /// Gets a collection of API scopes.
    /// </summary>
    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new ApiScope(AppScopes.PlannerAccess, "Access to DailyPlanner API")
    };
}