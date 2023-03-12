using DailyPlanner.Context;
using DailyPlanner.Context.Entities.User;
using DailyPlanner.Identity.Configuration.IdentityServer;
using Microsoft.AspNetCore.Identity;

namespace DailyPlanner.Identity.Configuration;

/// <summary>
/// The IdentityServer configuration.
/// </summary>
public static class IdentityServerConfiguration 
{
    /// <summary>
    /// Adds IdentityServer services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection instance.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppIdentityServer(this IServiceCollection services)
    {
        services
            .AddIdentity<User, UserRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<DailyPlannerContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services
            .AddIdentityServer()

            .AddAspNetIdentity<User>()

            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
            .AddInMemoryClients(AppClients.Clients)
            .AddInMemoryApiResources(AppResources.Resources)
            .AddInMemoryIdentityResources(AppIdentityResources.Resources)

            .AddDeveloperSigningCredential();

        return services;
    }
}