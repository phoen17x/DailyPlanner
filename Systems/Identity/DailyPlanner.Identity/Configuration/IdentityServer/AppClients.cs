using DailyPlanner.Common.Security;
using Duende.IdentityServer.Models;

namespace DailyPlanner.Identity.Configuration.IdentityServer;

/// <summary>
/// Provides a collection of clients that can access the application's APIs.
/// </summary>
public static class AppClients
{
    /// <summary>
    /// Gets a collection of clients.
    /// </summary>
    public static IEnumerable<Client> Clients => new List<Client>
    {
        new Client
        {
            ClientId = "swagger",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { AppScopes.PlannerAccess }
        },
        new Client
        {
            ClientId = "frontend",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowOfflineAccess = true,
            RefreshTokenUsage = TokenUsage.OneTimeOnly,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            AllowedScopes = { AppScopes.PlannerAccess }
        }
    };
}