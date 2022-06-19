using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationQlApp.InternalEvents;

public static class InternalEventsExtension
{
    public static IServiceCollection AddInternalEventsServices(this IServiceCollection services)
    {
        ServiceCollectionExtensions.AddMediatR((IServiceCollection)services, Assembly.GetExecutingAssembly());
        return services;
    }
}