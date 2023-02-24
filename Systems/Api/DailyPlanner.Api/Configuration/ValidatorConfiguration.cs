using DailyPlanner.Common.Extensions;
using DailyPlanner.Common.Responses;
using DailyPlanner.Common.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Api.Configuration;

/// <summary>
/// Validator configuration.
/// </summary>
public static class ValidatorConfiguration
{
    /// <summary>
    /// Registers app validators.
    /// </summary>
    /// <param name="builder">The <see cref="IMvcBuilder"/> instance.</param>
    /// <returns>The <see cref="IMvcBuilder"/> instance.</returns>
    public static IMvcBuilder AddAppValidator(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var fieldErrors = new List<ErrorResponseFieldInfo>();
                foreach (var (field, state) in context.ModelState)
                {
                    if (state.ValidationState == ModelValidationState.Invalid)
                        fieldErrors.Add(new ErrorResponseFieldInfo()
                        {
                            FieldName = field.ToCamelCase(),
                            Message = string.Join(", ", state.Errors.Select(x => x.ErrorMessage))
                        });
                }

                var result = new BadRequestObjectResult(new ErrorResponse()
                {
                    ErrorCode = 100,
                    Message = "One or more validation errors occurred.",
                    FieldErrors = fieldErrors
                });

                return result;
            };
        });

        builder.AddFluentValidation(fv =>
        {
            fv.DisableDataAnnotationsValidation = true;
            fv.ImplicitlyValidateChildProperties = true;
            fv.AutomaticValidationEnabled = true;
        });

        var services = builder.Services;

        var validators = from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
            where !type.IsAbstract && !type.IsGenericTypeDefinition
            let interfaces = type.GetInterfaces()
            let genericInterfaces = interfaces.Where(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>))
            let matchingInterface = genericInterfaces.FirstOrDefault()
            where matchingInterface != null
            select new
            {
                InterfaceType = matchingInterface,
                ValidatorType = type
            };

        validators.ToList().ForEach(validator => services.AddSingleton(validator.InterfaceType, validator.ValidatorType));

        builder.Services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return builder;
    }
}