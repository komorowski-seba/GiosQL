using Application.ExternalEvents;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Kafka;

public static class KafkaExtensions
{
    public static IServiceCollection AddKafkaPublishServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IExternalEventService<>), typeof(KafkaExternalEventPushService<>));
        return services;
    }

    public static IServiceCollection AddKafkaConsumerServices(this IServiceCollection services)
    {
        services.AddHostedService<KafkaExternalEventConsumerService<NewStationExtEvent>>();
        services.AddHostedService<KafkaExternalEventConsumerService<StationStatusExtEvent>>();
        return services;
    }
}