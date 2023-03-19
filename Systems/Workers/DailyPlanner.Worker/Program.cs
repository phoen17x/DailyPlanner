using DailyPlanner.Context;
using DailyPlanner.Worker.Configuration;
using DailyPlanner.Worker.TaskExecutor;
using DailyPlanner.Worker;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();


var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppDbContext(builder.Configuration);
services.RegisterAppServices();


var app = builder.Build();
app.Services.GetRequiredService<ITaskExecutor>().Start();
app.Run();