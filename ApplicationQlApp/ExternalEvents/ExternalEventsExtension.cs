using System.Reflection;

namespace ApplicationQlApp.ExternalEvents;

public static class ExternalEventsExtension
{
    public static IServiceCollection AddExternalEventsServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}