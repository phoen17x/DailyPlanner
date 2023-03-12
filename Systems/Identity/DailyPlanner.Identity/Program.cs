using DailyPlanner.Context;
using DailyPlanner.Identity.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();


var services = builder.Services;
services.AddAppCors();
services.AddHttpContextAccessor();
services.AddAppDbContext(builder.Configuration);
services.AddAppIdentityServer();


var app = builder.Build();
app.UseCors();
app.UseIdentityServer();
app.Run();
