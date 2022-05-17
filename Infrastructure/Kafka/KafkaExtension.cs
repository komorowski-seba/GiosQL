using Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Kafka;

public static class KafkaExtensions
{
    public static IServiceCollection AddKafkaPublishServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddConsumerConfig(configuration);
        services.AddScoped(
            typeof(IExternalEventService<>), 
            typeof(KafkaExternalEventPushService<>));
        return services;
    }

    public static IServiceCollection AddKafkaConsumerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddConsumerConfig(configuration);
        services.AddHostedService<KafkaExternalEventConsumerService<IExternalEvent>>();
        return services;
    }

    private static IServiceCollection AddConsumerConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(serviceProvider =>
        {
            var result = new ConsumerConfig
            {
                BootstrapServers = configuration.GetValue<string>("Kafka:BootstrapServer"),
                GroupId = configuration.GetValue<string>("Kafka:GroupId"),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };
            return result;
        });
        return services;
    }
}