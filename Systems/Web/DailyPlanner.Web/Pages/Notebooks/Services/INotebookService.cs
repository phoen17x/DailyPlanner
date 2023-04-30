using DailyPlanner.Web.Pages.Notebooks.Models;

namespace DailyPlanner.Web.Pages.Notebooks.Services;

public interface INotebookService
{
    /// <summary>
    /// Gets all the notebooks.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Notebook"/> representing all the notebooks.</returns>
    Task<IEnumerable<Notebook>> GetNotebooks();

    /// <summary>
    /// Gets the notebook with the specified ID.
    /// </summary>
    /// <param name="notebookId">The ID of the notebook to retrieve.</param>
    /// <returns>A <see cref="Notebook"/> object representing the notebook with the specified ID.</returns>
    Task<Notebook> GetNotebook(int notebookId);

    /// <summary>
    /// Adds a new notebook.
    /// </summary>
    /// <param name="model">The <see cref="NotebookModel"/> representing the notebook to add.</param>
    Task AddNotebook(NotebookModel model);

    /// <summary>
    /// Edits the notebook with the specified ID.
    /// </summary>
    /// <param name="notebookId">The ID of the notebook to edit.</param>
    /// <param name="model">The <see cref="NotebookModel"/> representing the notebook with the updated information.</param>
    Task EditNotebook(int notebookId, NotebookModel model);

    /// <summary>
    /// Deletes the notebook with the specified ID.
    /// </summary>
    /// <param name="notebookId">The ID of the notebook to delete.</param>
    Task DeleteNotebook(int notebookId);
}