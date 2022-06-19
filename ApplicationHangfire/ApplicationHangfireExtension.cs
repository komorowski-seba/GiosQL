using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationHangfire;

public static class ApplicationHangfireExtension
{
    public static IServiceCollection AddApplicationHangfireServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}