using System.Reflection;
using System.Text;
using System.Text.Json;
using DailyPlanner.Web.Pages.Accounts.Models;

namespace DailyPlanner.Web.Pages.Accounts.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient httpClient;

    public AccountService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<AccountResult> Register(SignUpModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/accounts";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<AccountResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new AccountResult();
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<string> GetPasswordRecoveryToken()
    {
        var url = $"{Settings.ApiRoot}/v1/accounts/password-recovery-token";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<string>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? string.Empty;
    }

    public async Task<AccountResult> SendPasswordRecoveryLink(SendEmailWithLinkModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/accounts/forgot-password";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        AccountResult result;
        try
        {
            result = JsonSerializer.Deserialize<AccountResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new AccountResult();
        }
        catch
        {
            result = new AccountResult();
        }
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<AccountResult> ChangePassword(ChangePasswordModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/accounts/change-password";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        AccountResult result;
        try
        {
            result = JsonSerializer.Deserialize<AccountResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new AccountResult();
        }
        catch
        {
            result = new AccountResult();
        }
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<UserModel> GetUserData()
    {
        var url = $"{Settings.ApiRoot}/v1/accounts/user";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);

        var data = JsonSerializer.Deserialize<UserModel>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new UserModel();

        return data;
    }

    public async Task<AccountResult> ConfirmEmail(ConfirmEmailModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/accounts/confirm-email";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        AccountResult result;
        try
        {
            result = JsonSerializer.Deserialize<AccountResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new AccountResult();
        }
        catch
        {
            result = new AccountResult();
        }
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }

    public async Task<AccountResult> SendConfirmationEmail(SendEmailWithLinkModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/accounts/confirmation-email";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        AccountResult result;
        try
        {
            result = JsonSerializer.Deserialize<AccountResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new AccountResult();
        }
        catch
        {
            result = new AccountResult();
        }
        result.IsSuccessful = response.IsSuccessStatusCode;

        return result;
    }
}