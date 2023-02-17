using DailyPlanner.Api;
using DailyPlanner.Api.Configuration;
using DailyPlanner.Context;
using DailyPlanner.Services.Settings;
using DailyPlanner.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();


var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");


var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddAppDbContext();
services.AddAppVersioning();
services.AddAppSwagger(swaggerSettings);
services.AddAppControllers();
services.RegisterAppServices();


var app = builder.Build();
app.UseAppCors();
app.UseAppSwagger();
app.MapControllers();
app.Run();
