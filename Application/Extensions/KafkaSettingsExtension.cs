using Microsoft.Extensions.Configuration;

namespace Application.Extensions;

public static class KafkaSettingsExtension
{
    public static string GetSettingsKafkaBootstrapServer(this IConfiguration configuration)
        => configuration.GetValue<string>("Kafka:BootstrapServer");

    public static string GetSettingsKafkaTopic(this IConfiguration configuration)
        => configuration.GetValue<string>("Kafka:Topic");

    public static string GetSettingsKafkaKey(this IConfiguration configuration)
        => configuration.GetValue<string>("Kafka:Key");
}