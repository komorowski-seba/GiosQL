using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalEvents;

public static class ExternalEventsExtension
{
    public static IServiceCollection AddExternalEventsServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}