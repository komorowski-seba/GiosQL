using Microsoft.Extensions.Configuration;

namespace Application.Extensions;

public static class KafkaSettingsExtension
{
    public static string SettingsKafkaBootstrapServer(this IConfiguration configuration)
        => configuration.GetValue<string>("Kafka:BootstrapServer");

    public static string SettingsKafkaTopic(this IConfiguration configuration)
        => configuration.GetValue<string>("Kafka:Topic");
}