using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InternalEvents;

public static class InternalEventsExtension
{
    public static IServiceCollection AddInternalEventsServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}