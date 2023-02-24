using System.Text.Json;
using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Extensions;
using DailyPlanner.Common.Responses;

namespace DailyPlanner.Api.Middleware;

/// <summary>
/// Middleware to handle exceptions and send appropriate error response to client.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// Invokes the middleware to catch and handle exceptions.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> to be processed.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse? response = null;

        try
        {
            await next.Invoke(context);
        }
        catch (ProcessException ex)
        {
            response = ex.ToErrorResponse();
        }
        catch (Exception ex)
        {
            response = ex.ToErrorResponse();
        }
        finally
        {
            if (response is not null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
            }
        }
    }
}