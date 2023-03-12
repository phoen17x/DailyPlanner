using Serilog;

namespace DailyPlanner.Identity.Configuration;

/// <summary>
/// The logger configuration.
/// </summary>
public static class LoggerConfiguration
{
    /// <summary>
    /// Adds the logger configuration to the application.
    /// </summary>
    /// <param name="builder">The web application builder instance.</param>
    public static void AddAppLogger(this WebApplicationBuilder builder)
    {
        var logger = new Serilog.LoggerConfiguration()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog(logger, true);
    }
}