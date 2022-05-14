using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HandlersQlApp;

public static class AppQLMediatorExtension
{
    public static IServiceCollection AddAppQLMediatorServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}