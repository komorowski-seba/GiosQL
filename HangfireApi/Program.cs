using ApplicationHangfire;
using Infrastructure;
using Infrastructure.Elastic;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseConfigSeriLog(builder.Configuration, builder.Environment.EnvironmentName);

var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddHangfireInfrastructureServices(builder.Configuration);
services.AddApplicationHangfireServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseHangfireInfrastructure();
app.MapGet("/", (HttpContext context) 
    => $" Hangfire - https://{context.Response.HttpContext.Request.Host.Host}:{context.Response.HttpContext.Request.Host.Port}/Hangfire"
    +  $"\n Redis commander - http://localhost:8081/");
app.Run();