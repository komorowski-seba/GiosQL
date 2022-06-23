using System.Reflection;
using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

    public static IHostBuilder UseConfigSeriLog(
        this IHostBuilder builder, 
        IConfiguration config, 
        string environmentName)
    {
        builder.UseSerilog();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .Enrich.FromLogContext()  
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config["Elasticsearch:Url"]))  
            {  
                AutoRegisterTemplate = true,  
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{DateTime.UtcNow:yyyy-MM}"  
            })  
            .Enrich.WithProperty("Environment", environmentName)  
            .ReadFrom.Configuration(config)  
            .CreateLogger();  
        return builder;
    }
}
