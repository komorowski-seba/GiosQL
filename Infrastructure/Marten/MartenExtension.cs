using Application.Interfaces;
using Infrastructure.Marten.Projections;
using Infrastructure.Marten.Services;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;

namespace Infrastructure.Marten;

public static class MartenExtension
{
    public static IServiceCollection AddMartenServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMarten(n =>
        {
            n.Connection(configuration.GetConnectionString("Marten"));
            n.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
            n.DatabaseSchemaName = "Gios_db";
            n.Events.DatabaseSchemaName = "Gios_events";
            n.Projections.Add<StationEventsProjection>();
        });
        services.AddScoped<IEventsStoreDb, MartensEventsStoreDb>();
        return services;
    }
}