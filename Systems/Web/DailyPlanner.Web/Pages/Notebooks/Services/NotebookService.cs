using System.Text;
using System.Text.Json;
using DailyPlanner.Web.Pages.Notebooks.Models;

namespace DailyPlanner.Web.Pages.Notebooks.Services;

public class NotebookService : INotebookService
{
    private readonly HttpClient httpClient;

    public NotebookService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Notebook>> GetNotebooks()
    {
        var url = $"{Settings.ApiRoot}/v1/notebooks";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);

        var data = JsonSerializer.Deserialize<IEnumerable<Notebook>>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Notebook>();

        return data;
    }

    public async Task<Notebook> GetNotebook(int notebookId)
    {
        var url = $"{Settings.ApiRoot}/v1/notebooks/{notebookId}";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);

        var data = JsonSerializer.Deserialize<Notebook>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new Notebook();

        return data;
    }

    public async Task AddNotebook(NotebookModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/notebooks";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }

    public async Task EditNotebook(int notebookId, NotebookModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/notebooks/{notebookId}";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }

    public async Task DeleteNotebook(int notebookId)
    {
        var url = $"{Settings.ApiRoot}/v1/notebooks/{notebookId}";

        var response = await httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }
}