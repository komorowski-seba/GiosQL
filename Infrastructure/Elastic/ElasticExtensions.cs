using System.Reflection;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Infrastructure.Elastic;

public static class ElasticExtensions
{
    public static IServiceCollection AddElasticServices(this IServiceCollection services, IConfiguration configuration)
    {
        // var settings = new ConnectionSettings(new Uri(configuration["Elasticsearch:Url"]))
        //     .DefaultMappingFor<AirTestElasticModel>(n => n.IndexName(configuration["Elasticsearch:Index"]));
        // var client = new ElasticClient(settings);
        //     
        // services.AddSingleton<IElasticClient>(client);            
        // services.AddScoped<ISearchService, ElasticSearchService>();
        return services;
    }

    public static void SetConfigureLogging(IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        // var configuration = new ConfigurationBuilder()
        //     .AddJsonFile(
        //         "appsettings.json", 
        //         optional: false, 
        //         reloadOnChange: true)
        //     .AddJsonFile(
        //         $"appsettings.{environment}.json",
        //         optional: true)
        //     .Build();
        
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
    
    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["Elasticsearch:Url"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        };
    }
}
