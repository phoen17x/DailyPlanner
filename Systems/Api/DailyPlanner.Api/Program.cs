using DailyPlanner.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();


var services = builder.Services;
services.AddControllers();


var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
