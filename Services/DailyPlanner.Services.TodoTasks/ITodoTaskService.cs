using DailyPlanner.Services.TodoTasks.Models;

namespace DailyPlanner.Services.TodoTasks;

public interface ITodoTaskService
{
    /// <summary>
    /// Gets a collection of todotasks.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="notebookId">ID of the notebook to get todotasks from.</param>
    /// <returns>Collection of <see cref="TodoTaskModel"/> objects.</returns>
    Task<IEnumerable<TodoTaskModel>> GetTodoTasks(Guid userId, int notebookId);

    /// <summary>
    /// Gets a single todotask by ID.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="todoTaskId">ID of the todotask to retrieve.</param>
    /// <returns><see cref="TodoTaskModel"/> object.</returns>
    Task<TodoTaskModel> GetTodoTask(Guid userId, int todoTaskId);

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
    /// <param name="userId">User identifier.</param>
    /// <param name="todoTaskId">ID of the todotask to delete.</param>
    Task DeleteTodoTask(Guid userId, int todoTaskId);
}