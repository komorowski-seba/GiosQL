using ApplicationHangfire;
using Infrastructure;
using Infrastructure.Hangfire;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddHangfireServices(builder.Configuration);
services.AddHangfireInfrastructureServices(builder.Configuration);
services.AddApplicationHangfireServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseHangfireConfiguration();

app.MapGet("/", (HttpContext context) 
    => $" Hangfire - https://{context.Response.HttpContext.Request.Host.Host}:{context.Response.HttpContext.Request.Host.Port}/Hangfire"
                                         + $"\n Redis commander - http://localhost:8081/");

app.Run();