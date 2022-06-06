using Application;
using ExternalEvents;
using HandlersQlApp;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.QL;
using InternalEvents;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Elasticsearch(
            new ElasticsearchSinkOptions(new Uri(""))
            {
                IndexFormat = "",//$"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                AutoRegisterTemplate = true,
                NumberOfShards = 2,
                NumberOfReplicas = 1
            })
        .Enrich.WithProperty("Environment", "")
        .ReadFrom.Configuration("context configuration")
});

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddApplicationServices();
services.AddAppQLMediatorServices();
services.AddExternalEventsServices();
services.AddInternalEventsServices();
// services.AddSwaggerGen();
services.AddWebQlInfrastructureServices(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}
app.UsePersistenceConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseQlConfiguration(app.Environment.IsDevelopment());
app.Run();