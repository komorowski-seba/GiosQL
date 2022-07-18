using Application.Extensions;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Redis;

public static class RedisExtension
{
    public static IServiceCollection AddRedisServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = configuration.GetSettingsRedisConfigurationHost();
            option.InstanceName = configuration.GetSettingsRedisInstanceName(); 
        });
        services.AddScoped<ICacheService, RedisCacheService>();
        return services;
    }
}