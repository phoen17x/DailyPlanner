using DailyPlanner.Services.Notebooks.Models;

namespace DailyPlanner.Services.Notebooks;

public interface INotebookService
{
    /// <summary>
    /// Gets a collection of notebooks.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <returns>Collection of <see cref="NotebookModel"/> objects.</returns>
    Task<IEnumerable<NotebookModel>> GetNotebooks(Guid userId);

    /// <summary>
    /// Gets a single notebook by ID.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="notebookId">ID of the notebook to retrieve.</param>
    /// <returns><see cref="NotebookModel"/> object.</returns>
    Task<NotebookModel> GetNotebook(Guid userId, int notebookId);

    /// <summary>
    /// Adds a new notebook.
    /// </summary>
    /// <param name="model">Data for the new notebook.</param>
    /// <returns>The newly created <see cref="NotebookModel"/> object.</returns>
    Task<NotebookModel> AddNotebook(AddNotebookModel model);

    /// <summary>
    /// Updates an existing notebook.
    /// </summary>
    /// <param name="notebookId">ID of the notebook to update.</param>
    /// <param name="model">New data for the notebook.</param>
    Task UpdateNotebook(int notebookId, UpdateNotebookModel model);

    /// <summary>
    /// Deletes a notebook.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="notebookId">ID of the notebook to delete.</param>
    Task DeleteNotebook(Guid userId, int notebookId);
}