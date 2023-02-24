using DailyPlanner.Common.Exceptions;
using DailyPlanner.Common.Responses;

namespace DailyPlanner.Common.Extensions;

/// <summary>
/// Provides extension methods for exceptions to convert them to an <see cref="ErrorResponse"/>.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Converts a <see cref="ProcessException"/> to an <see cref="ErrorResponse"/> with the exception message.
    /// </summary>
    /// <param name="exception">The <see cref="ProcessException"/> to convert.</param>
    /// <returns>An <see cref="ErrorResponse"/> object with the exception message.</returns>
    public static ErrorResponse ToErrorResponse(this ProcessException exception)
    {
        return new ErrorResponse() { Message = exception.Message };
    }

    /// <summary>
    /// Converts an <see cref="Exception"/> to an <see cref="ErrorResponse"/> with an error code of -1 and the exception message.
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/> to convert.</param>
    /// <returns>An <see cref="ErrorResponse"/> object with the error code and exception message.</returns>
    public static ErrorResponse ToErrorResponse(this Exception exception)
    {
        return new ErrorResponse() { ErrorCode = -1, Message = exception.Message };
    }
}