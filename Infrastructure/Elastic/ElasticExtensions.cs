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
        // var environment = Environment
        //     .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?
        //     .ToLower()
        //     .Replace(".", "-") ?? throw new ApplicationException("[Error no environment]");
        // var name = Assembly
        //     .GetExecutingAssembly()
        //     .GetName()
        //     .Name?
        //     .ToLower()
        //     .Replace(".", "-") ?? throw new ApplicationException("[Error no assembly name]");

        // builder.UseSerilog((context, configuration) =>
        // {
        //     configuration
        //         .WriteTo.Console()
        //         .Enrich.FromLogContext()
        //         .Enrich.WithMachineName()
        //         .WriteTo.Elasticsearch(
        //             new ElasticsearchSinkOptions(new Uri(config["Elasticsearch:Url"]))
        //             {
        //                 IndexFormat = $"{name}-{environment}-{DateTime.UtcNow:yyyy-MM}",
        //                 AutoRegisterTemplate = true,
        //                 NumberOfShards = 2,
        //                 NumberOfReplicas = 1
        //             })
        //         .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        //         .ReadFrom.Configuration(context.Configuration);
        // });
        builder.UseSerilog();
        Log.Logger = new LoggerConfiguration()
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
        
        // // var configuration = new ConfigurationBuilder()
        // //     .AddJsonFile(
        // //         "appsettings.json", 
        // //         optional: false, 
        // //         reloadOnChange: true)
        // //     .AddJsonFile(
        // //         $"appsettings.{environment}.json",
        // //         optional: true)
        // //     .Build();
        //
        // Log.Logger = new LoggerConfiguration()
        //     .Enrich.FromLogContext()
        //     .Enrich.WithMachineName()
        //     .WriteTo.Debug()
        //     .WriteTo.Console()
        //     .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        //     .Enrich.WithProperty("Environment", environment)
        //     .ReadFrom.Configuration(configuration)
        //     .CreateLogger();
    }
    
    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["Elasticsearch:Url"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        };
    }
}
