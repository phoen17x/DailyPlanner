using DailyPlanner.Api.Middleware;

namespace DailyPlanner.Api.Configuration;

/// <summary>
/// Configures the application to use custom middleware.
/// </summary>
public static class MiddlewareConfiguration
{
    /// <summary>
    /// Adds middleware to the application's request pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    public static void UseAppMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}