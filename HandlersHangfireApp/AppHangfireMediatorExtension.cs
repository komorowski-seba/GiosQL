using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HandlersHangfireApp;

public static class AppHangfireMediatorExtension
{
    public static IServiceCollection AddHangfireMediatorServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}