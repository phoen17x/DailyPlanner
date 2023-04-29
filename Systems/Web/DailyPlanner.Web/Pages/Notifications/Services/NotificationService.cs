using DailyPlanner.Web.Pages.Notifications.Models;
using System.Text;
using System.Text.Json;
using DailyPlanner.Web.JsonConverters;

namespace DailyPlanner.Web.Pages.Notifications.Services;

public class NotificationService : INotificationService
{
    private readonly HttpClient httpClient;

    public NotificationService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Notification>> GetNotifications()
    {
        var url = $"{Settings.ApiRoot}/v1/notifications";

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);

        var data = JsonSerializer.Deserialize<IEnumerable<Notification>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new DateTimeConverter("dd/MM/yyyy HH:mm") } }) ?? new List<Notification>();

        return data;
    }

    public async Task MarkAsRead(int notificationId)
    {
        var url = $"{Settings.ApiRoot}/v1/notifications/mark-as-read/{notificationId}";

        var request = new StringContent("", Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }

    public async Task MarkAllAsRead()
    {
        var url = $"{Settings.ApiRoot}/v1/notifications/mark-as-read";

        var request = new StringContent("", Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }

    public async Task DeleteNotification(int notificationId)
    {
        var url = $"{Settings.ApiRoot}/v1/notifications/{notificationId}";

        var response = await httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }

    public async Task DeleteAllNotifications()
    {
        var url = $"{Settings.ApiRoot}/v1/notifications";

        var response = await httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode == false) throw new Exception(content);
    }
}