using Blazored.LocalStorage;
using DailyPlanner.Web;
using DailyPlanner.Web.Pages.Accounts.Services;
using DailyPlanner.Web.Pages.Auth.Services;
using DailyPlanner.Web.Pages.Notebooks.Services;
using DailyPlanner.Web.Pages.TodoTasks.Services;
using DailyPlanner.Web.Providers;
using DailyPlanner.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<INotebookService, NotebookService>();
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();

await builder.Build().RunAsync();
