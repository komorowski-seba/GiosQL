using Infrastructure.Elastic;
using Infrastructure.Kafka;
using Infrastructure.Marten;
using Infrastructure.Persistence;
using Infrastructure.QL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureWebQlExtension
{
    public static IServiceCollection AddWebQlInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPersistenceServices(configuration);
        services.AddKafkaConsumerServices(configuration);
        services.AddQLServices();
        services.AddMartenServices(configuration);
        services.AddElasticServices(configuration);
        return services;
    }
}