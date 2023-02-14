namespace DailyPlanner.Services.Settings;

/// <summary>
/// Represents the settings for the Swagger.
/// </summary>
public class SwaggerSettings
{
    public bool Enabled { get; private set; }
    public string? OAuthClientId { get; private set; }
    public string? OAuthClientSecret { get; private set; }

    public SwaggerSettings()
    {
        Enabled = false;
    }
}