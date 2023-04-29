using DailyPlanner.Api;
using DailyPlanner.Api.Configuration;
using DailyPlanner.Context;
using DailyPlanner.Context.Setup;
using DailyPlanner.Services.Settings;
using DailyPlanner.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();


var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");


var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddAppDbContext();
services.AddAppAuth(identitySettings);
services.AddAppVersioning();
services.AddAppSwagger(swaggerSettings, identitySettings);
services.AddAppAutoMapper();
services.AddAppSignalR();
services.AddAppControllers();
services.RegisterAppServices();


var app = builder.Build();
app.UseAppCors();
app.UseAppSwagger();
app.UseAppAuth();
app.UseAppSignalR();
app.MapControllers();
app.UseAppMiddleware();

DbInitializer.Execute(app.Services);

app.Run();
