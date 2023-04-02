namespace DailyPlanner.Web.Services;

public interface IConfigurationService
{
    Task<bool> GetDarkMode();
    Task SetDarkMode(bool value);
}