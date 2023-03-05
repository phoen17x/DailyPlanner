using DailyPlanner.Services.TodoTasks.Models;

namespace DailyPlanner.Services.TodoTasks;

public interface ITodoTaskService
{
    /// <summary>
    /// Gets a collection of todotasks.
    /// </summary>
    /// <param name="notebookId">ID of the notebook to get todotasks from.</param>
    /// <returns>Collection of <see cref="TodoTaskModel"/> objects.</returns>
    Task<IEnumerable<TodoTaskModel>> GetTodoTasks(int notebookId);

    /// <summary>
    /// Adds a new todotask.
    /// </summary>
    /// <param name="model">Data for the new todotask.</param>
    /// <returns>The newly created <see cref="TodoTaskModel"/> object.</returns>
    Task<TodoTaskModel> AddTodoTask(AddTodoTaskModel model);

    /// <summary>
    /// Updates an existing todotask.
    /// </summary>
    /// <param name="todoTaskId">ID of the todotask to update.</param>
    /// <param name="model">New data for the todotask.</param>
    Task UpdateTodoTask(int todoTaskId, UpdateTodoTaskModel model);

    /// <summary>
    /// Deletes a todotask.
    /// </summary>
    /// <param name="todoTaskId">ID of the todotask to delete.</param>
    Task DeleteTodoTask(int todoTaskId);
}