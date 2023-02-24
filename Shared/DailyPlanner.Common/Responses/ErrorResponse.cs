namespace DailyPlanner.Common.Responses;

/// <summary>
/// Represents an error response that can be returned to the client in case of errors.
/// </summary>
public class ErrorResponse
{
    public int ErrorCode { get; set; }
    public string Message { get; set; }
    public IEnumerable<ErrorResponseFieldInfo> FieldErrors { get; set; }
}

/// <summary>
/// Represents field information for a specific error response.
/// </summary>
public class ErrorResponseFieldInfo
{
    public string FieldName { get; set; }
    public string Message { get; set; }
}