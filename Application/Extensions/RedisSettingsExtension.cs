using Microsoft.Extensions.Configuration;

namespace Application.Extensions;

public static class RedisSettingsExtension
{
    public static string GetSettingsRedisConfigurationHost(this IConfiguration configuration)
        => configuration.GetValue<string>("Redis:ConfigurationHost");

    public static string GetSettingsRedisInstanceName(this IConfiguration configuration)
        => configuration.GetValue<string>("Redis:InstanceName");
}