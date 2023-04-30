using DailyPlanner.Web.Pages.TodoTasks.Models;

namespace DailyPlanner.Web.Pages.TodoTasks.Services;

public interface ITodoTaskService
{
    /// <summary>
    /// Retrieves all todotasks.
    /// </summary>
    /// <returns>An enumerable collection of todotasks.</returns>
    Task<IEnumerable<TodoTask>> GetTodoTasks();

    /// <summary>
    /// Retrieves all todotasks associated with a particular notebook.
    /// </summary>
    /// <param name="notebookId">The ID of the notebook to retrieve todotasks for.</param>
    /// <returns>An enumerable collection of todotasks associated with the specified notebook.</returns>
    Task<IEnumerable<TodoTask>> GetTodoTasks(int notebookId);

    /// <summary>
    /// Retrieves a todotask by ID.
    /// </summary>
    /// <param name="todoTaskId">The ID of the todotask to retrieve.</param>
    /// <returns>The todotask with the specified ID.</returns>
    Task<TodoTask> GetTodoTask(int todoTaskId);

    /// <summary>
    /// Adds a new todotask.
    /// </summary>
    /// <param name="model">The data model for the new todotask.</param>
    Task AddTodoTask(TodoTaskModel model);

    /// <summary>
    /// Updates an existing todotask.
    /// </summary>
    /// <param name="todoTaskId">The ID of the todotask to update.</param>
    /// <param name="model">The updated data model for the todotask.</param>
    Task EditTodoTask(int todoTaskId, TodoTaskModel model);

    /// <summary>
    /// Deletes a todotask by ID.
    /// </summary>
    /// <param name="todoTaskId">The ID of the todotask to delete.</param>
    Task DeleteTodoTask(int todoTaskId);
}