using HandlersHangfireApp;
using HangfireInfrastructure;
using Infrastructure.Gios;
using Infrastructure.Kafka;
using Infrastructure.Redis;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddHangfireServices(builder.Configuration);
services.AddGiosServices();
services.AddKafkaPublishServices(builder.Configuration);
services.AddRedisServices(builder.Configuration);
services.AddHangfireMediatorServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseHangfireConfiguration();
app.Run();