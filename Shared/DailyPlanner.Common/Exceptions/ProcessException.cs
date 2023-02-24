namespace DailyPlanner.Common.Exceptions;

/// <summary>
/// Custom exception class for errors that occur during processing.
/// </summary>
public class ProcessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ProcessException(string message) : base(message) {}

    /// <summary>
    /// Throws a <see cref="ProcessException"/> if the specified object is null.
    /// </summary>
    /// <param name="obj">The object to check for null.</param>
    /// <param name="message">The message to include in the exception if the object is null.</param>
    public static void ThrowIfNull(object? obj, string message)
    {
        if (obj is null) throw new ProcessException(message);
    }
}