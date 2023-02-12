using DailyPlanner.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();


var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddAppVersioning();
services.AddAppSwagger();
services.AddAppControllers();


var app = builder.Build();
app.UseAppCors();
app.UseAppSwagger();
app.MapControllers();
app.Run();
