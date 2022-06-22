using Infrastructure.Elastic;
using Infrastructure.Gios;
using Infrastructure.Hangfire;
using Infrastructure.Kafka;
using Infrastructure.Marten;
using Infrastructure.Persistence;
using Infrastructure.QL;
using Infrastructure.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureExtension
{
    public static IServiceCollection AddHangfireInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHangfireServices(configuration);
        services.AddGiosServices();
        services.AddKafkaPublishServices(configuration);
        services.AddRedisServices(configuration);
        return services;
    }

    public static IApplicationBuilder UseHangfireInfrastructure(this IApplicationBuilder app)
    {
        app.UseHangfireConfiguration();
        return app;
    }

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