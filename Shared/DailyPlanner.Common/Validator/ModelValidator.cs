using FluentValidation;

namespace DailyPlanner.Common.Validator;

/// <summary>
/// Provides a generic implementation of the <see cref="IModelValidator{T}"/>.
/// </summary>
/// <typeparam name="T">The type of model to validate.</typeparam>
public class ModelValidator<T> : IModelValidator<T> where T : class
{
    private readonly IValidator<T> validator;

    public ModelValidator(IValidator<T> validator)
    {
        this.validator = validator;
    }

    /// <summary>
    /// Validates the specified model and throws a <see cref="ValidationException"/> if the model is invalid.
    /// </summary>
    /// <param name="model">The model to validate.</param>
    /// <exception cref="ValidationException">Thrown if the model is invalid.</exception>
    public void Check(T model)
    {
        var validationResult = validator.Validate(model);
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult.Errors);
    }
}