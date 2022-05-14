using GraphQL.Server;
using Infrastructure.QL.Queries;
using Infrastructure.QL.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.QL;

public static class QLExtension
{
    public static IServiceCollection AddQLServices(this IServiceCollection services)
    {
        services.AddScoped<StationApiSchema>();
        services.AddScoped<StationQuery>();
        services
            .AddGraphQL()
            .AddGraphTypes(typeof(StationApiSchema), ServiceLifetime.Scoped)
            .AddSystemTextJson();
        services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        return services;
    }

    public static IApplicationBuilder UseQlConfiguration(this IApplicationBuilder app, bool isDevelopment)
    {
        app.UseGraphQL<StationApiSchema>();
        return app;
    }
}