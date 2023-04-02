using DailyPlanner.Web.Pages.Notebooks.Models;

namespace DailyPlanner.Web.Pages.Notebooks.Services;

public interface INotebookService
{
    Task<IEnumerable<Notebook>> GetNotebooks(int offset = 0, int limit = 10);
    Task<Notebook> GetNotebook(int notebookId);
    Task AddNotebook(NotebookModel model);
    Task EditNotebook(int notebookId, NotebookModel model);
    Task DeleteNotebook(int notebookId);
}