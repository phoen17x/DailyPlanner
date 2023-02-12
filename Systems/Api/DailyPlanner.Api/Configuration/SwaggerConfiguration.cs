using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DailyPlanner.Api.Configuration;

/// <summary>
/// The Swagger configuration.
/// </summary>
public static class SwaggerConfiguration
{
    private static string AppTitle = "Daily Planner API";

    /// <summary>
    /// Adds Swagger to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The services collection instance.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services
            .AddOptions<SwaggerGenOptions>()
            .Configure<IApiVersionDescriptionProvider>((options, provider) =>
            {
                foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(apiVersionDescription.GroupName, new OpenApiInfo
                    {
                        Title = AppTitle,
                        Version = apiVersionDescription.GroupName
                    });
                }
            });

        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.UseInlineDefinitionsForEnums();
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.DescribeAllParametersInCamelCase();

            var xmlPath = Path.Combine(AppContext.BaseDirectory, "api.xml");
            options.IncludeXmlComments(xmlPath);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Name = "Bearer",
                Type = SecuritySchemeType.OAuth2,
                Scheme = "oauth2",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Flows = new OpenApiOAuthFlows()
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new List<string>()
                }
            });

            options.UseOneOfForPolymorphism();
            options.EnableAnnotations(true, true);
            options.ExampleFilters();
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }

    /// <summary>
    /// Uses Swagger in the specified <see cref="WebApplication"/>.
    /// </summary>
    /// <param name="app">The web application.</param>
    public static void UseAppSwagger(this WebApplication app)
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger(options =>
        {
            options.RouteTemplate = "api/{documentName}/api.yaml";
        });

        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "api";

            provider.ApiVersionDescriptions.ToList().ForEach(
                apiVersionDescription => options.SwaggerEndpoint(
                    $"/api/{apiVersionDescription.GroupName}/api.yaml", 
                    apiVersionDescription.GroupName.ToUpperInvariant()
                )
            );

            options.DocExpansion(DocExpansion.List);
            options.DefaultModelsExpandDepth(-1);
            options.OAuthAppName(AppTitle);
        });
    }
}