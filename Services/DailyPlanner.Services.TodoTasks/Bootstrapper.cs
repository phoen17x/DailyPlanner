using Microsoft.Extensions.DependencyInjection;

namespace DailyPlanner.Services.TodoTasks;

public static class Bootstrapper
{
    public static IServiceCollection AddTodoTaskService(this IServiceCollection services)
    {
        services.AddSingleton<ITodoTaskService, TodoTaskService>();
        return services;
    }
}