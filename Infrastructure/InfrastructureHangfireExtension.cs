using Infrastructure.Gios;
using Infrastructure.Kafka;
using Infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureHangfireExtension
{
    public static IServiceCollection AddHangfireInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGiosServices();
        services.AddKafkaPublishServices(configuration);
        services.AddRedisServices(configuration);
        return services;
    }
}