using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

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
}
