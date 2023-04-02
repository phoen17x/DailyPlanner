using Blazored.LocalStorage;
using DailyPlanner.Web.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;
using DailyPlanner.Web.Pages.Auth.Models;
using System.IdentityModel.Tokens.Jwt;

namespace DailyPlanner.Web.Pages.Auth.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient httpClient;
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly ILocalStorageService localStorage;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        this.httpClient = httpClient;
        this.authenticationStateProvider = authenticationStateProvider;
        this.localStorage = localStorage;
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var url = $"{Settings.IdentityRoot}/connect/token";
        var requestBody = new KeyValuePair<string, string>[]
        {
            new("grant_type", "password"),
            new("client_id", Settings.ClientId),
            new("client_secret", Settings.ClientSecret),
            new("username", loginModel.Email),
            new("password", loginModel.Password)
        };

        var requestContent = new FormUrlEncodedContent(requestBody);
        var response = await httpClient.PostAsync(url, requestContent);
        var content = await response.Content.ReadAsStringAsync();

        var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
        loginResult.IsSuccessful = response.IsSuccessStatusCode;

        if (response.IsSuccessStatusCode == false) return loginResult;

        await localStorage.SetItemAsync("authToken", loginResult.AccessToken);
        await localStorage.SetItemAsync("refreshToken", loginResult.RefreshToken);

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

        return loginResult;
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync("authToken");
        await localStorage.RemoveItemAsync("refreshToken");

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
        httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task RefreshToken()
    {
        var refreshToken = await localStorage.GetItemAsync<string>("refreshToken");
        if (refreshToken is null)
        {
            await Logout();
            return;
        }


        var url = $"{Settings.IdentityRoot}/connect/token";
        var requestBody = new KeyValuePair<string, string>[]
        {
            new("grant_type", "refresh_token"),
            new("client_id", Settings.ClientId),
            new("client_secret", Settings.ClientSecret),
            new("refresh_token", refreshToken)
        };

        var requestContent = new FormUrlEncodedContent(requestBody);
        var response = await httpClient.PostAsync(url, requestContent);

        if (response.IsSuccessStatusCode == false)
        {
            await Logout();
            return;
        }

        var content = await response.Content.ReadAsStringAsync();
        var loginResult = JsonSerializer.Deserialize<LoginResult>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();

        await localStorage.SetItemAsync("authToken", loginResult.AccessToken);
        await localStorage.SetItemAsync("refreshToken", loginResult.RefreshToken);

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", loginResult.AccessToken);
    }
}