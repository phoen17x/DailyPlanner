using System.Text.Json.Serialization;

namespace DailyPlanner.Web.Pages.Accounts.Models;

public class SignUpResult
{
    public bool IsSuccessful { get; set; }
    [JsonPropertyName("fieldErrors")] public List<ErrorInfo> Errors { get; set; }
}

public class ErrorInfo
{
    public string FieldName { get; set; }
    public string Message { get; set; }
}