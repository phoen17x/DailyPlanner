using DailyPlanner.Web.Pages.TodoTasks.Models;
using System.Text;
using System.Text.Json;
using DailyPlanner.Web.JsonConverters;

namespace DailyPlanner.Web.Pages.TodoTasks.Services;

public class TodoTaskService : ITodoTaskService
{
    private readonly HttpClient httpClient;

    public TodoTaskService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<TodoTask>> GetTodoTasks()
    {
        var url = $"{Settings.ApiRoot}/v1/todotasks/all";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);

        var data = JsonSerializer.Deserialize<IEnumerable<TodoTask>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new DateTimeConverter("dd/MM/yyyy HH:mm") } }) ?? new List<TodoTask>();

        return data;
    }

    public async Task<IEnumerable<TodoTask>> GetTodoTasks(int notebookId)
    {
        var url = $"{Settings.ApiRoot}/v1/todotasks?notebookId={notebookId}";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);

        var data = JsonSerializer.Deserialize<IEnumerable<TodoTask>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new DateTimeConverter("dd/MM/yyyy HH:mm")}}) ?? new List<TodoTask>();

        return data;
    }

    public async Task<TodoTask> GetTodoTask(int todoTaskId)
    {
        var url = $"{Settings.ApiRoot}/v1/todotasks/{todoTaskId}";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);

        var data = JsonSerializer.Deserialize<TodoTask>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new DateTimeConverter("dd/MM/yyyy HH:mm")}}) ?? new TodoTask();

        return data;
    }

    public async Task AddTodoTask(TodoTaskModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/todotasks";

        var body = JsonSerializer.Serialize(model, new JsonSerializerOptions { Converters = { new DateTimeConverter("dd/MM/yyyy HH:mm") } });
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }

    public async Task EditTodoTask(int todoTaskId, TodoTaskModel model)
    {
        var url = $"{Settings.ApiRoot}/v1/todotasks/{todoTaskId}";

        var body = JsonSerializer.Serialize(model, new JsonSerializerOptions { Converters = { new DateTimeConverter("dd/MM/yyyy HH:mm") } });
        Console.WriteLine(body);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }

    public async Task DeleteTodoTask(int todoTaskId)
    {
        var url = $"{Settings.ApiRoot}/v1/todotasks/{todoTaskId}";

        var response = await httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }
}