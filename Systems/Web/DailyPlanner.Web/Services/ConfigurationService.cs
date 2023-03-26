using Blazored.LocalStorage;

namespace DailyPlanner.Web.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly ILocalStorageService localStorage;

    public ConfigurationService(ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    public async Task<bool> GetDarkMode()
    {
        return await localStorage.GetItemAsync<bool>("darkMode");
    }

    public async Task SetDarkMode(bool value)
    {
        await localStorage.SetItemAsync("darkMode", value);
    }

    public async Task<bool> GetNavigationMenuVisible()
    {
        return await localStorage.GetItemAsync<bool>("navigationMenuVisible");
    }

    public async Task SetNavigationMenuVisible(bool value)
    {
        await localStorage.SetItemAsync("navigationMenuVisible", value);
    }
}