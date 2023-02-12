using DailyPlanner.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();


var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddControllers();


var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.UseAppCors();
app.Run();
