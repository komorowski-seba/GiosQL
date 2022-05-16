﻿using Application.ExternalEvents;
using Application.Mapping;
using Domain.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalEvents;

public class NewStationEventHandler : INotificationHandler<NewStationExtEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public NewStationEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task Handle(NewStationExtEvent notification, CancellationToken cancellationToken)
    {

        var repo = _scopeFactory
            .CreateScope()
            .ServiceProvider
            .GetService<IStationRepository>();

        if (repo is null)
            throw new NullReferenceException(nameof(IStationRepository));
        
        var find = await repo.GetStationByIdentifierAsync(notification.NewStation.Id, cancellationToken);
        if (find is null)
        {
            await repo.AddStationAsync(notification.NewStation.ToEntityStation());
        }
        else
        {
            find
                .SetAddressStreet(notification.NewStation?.AddressStreet ?? string.Empty)
                .SetCityName(notification.NewStation.City?.Name ?? string.Empty)
                .SetDistrictName(notification.NewStation.City?.Commune?.DistrictName ?? string.Empty)
                .SetGegrLat(notification.NewStation.GegrLat)
                .SetGegrLon(notification.NewStation.GegrLon)
                .SetProvinceName(notification.NewStation.City?.Commune?.ProvinceName ?? string.Empty)
                .SetStationName(notification.NewStation.StationName);
        }

        await repo.SaveChangesAsync(cancellationToken);
    }
}