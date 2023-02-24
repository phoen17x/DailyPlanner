namespace DailyPlanner.Common.Validator;

/// <summary>
/// Defines the contract for validating an object of type T.
/// </summary>
/// <typeparam name="T">The type of object to validate.</typeparam>
public interface IModelValidator<T> where T : class
{
    /// <summary>
    /// Validates the given model of type T.
    /// </summary>
    /// <param name="model">The model to validate.</param>
    void Check(T model);
}