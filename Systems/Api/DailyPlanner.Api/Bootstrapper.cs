﻿using DailyPlanner.Services.Notebooks;
using DailyPlanner.Services.Settings;
using DailyPlanner.Services.TodoTasks;
using DailyPlanner.Services.UserAccount;

namespace DailyPlanner.Api;

/// <summary>
/// Responsible for registering application services in the dependency injection container.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Registers the application services in the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddSwaggerSettings()
            .AddIdentitySettings()
            .AddUserAccountService()
            .AddNotebookService()
            .AddTodoTaskService();

        return services;
    }
}