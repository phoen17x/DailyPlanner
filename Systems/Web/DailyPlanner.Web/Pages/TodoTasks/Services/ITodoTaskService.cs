using DailyPlanner.Web.Pages.TodoTasks.Models;

namespace DailyPlanner.Web.Pages.TodoTasks.Services;

public interface ITodoTaskService
{
    Task<IEnumerable<TodoTask>> GetTodoTasks();
    Task<IEnumerable<TodoTask>> GetTodoTasks(int notebookId);
    Task<TodoTask> GetTodoTask(int todoTaskId);
    Task AddTodoTask(TodoTaskModel model);
    Task EditTodoTask(int todoTaskId, TodoTaskModel model);
    Task DeleteTodoTask(int todoTaskId);
}