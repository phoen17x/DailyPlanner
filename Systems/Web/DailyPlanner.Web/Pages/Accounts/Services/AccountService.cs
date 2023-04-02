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

    public async Task<SignUpResult> Register(SignUpModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/accounts";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        var signUpResult = JsonSerializer.Deserialize<SignUpResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new SignUpResult();
        signUpResult.IsSuccessful = response.IsSuccessStatusCode;

        return signUpResult;
    }
}