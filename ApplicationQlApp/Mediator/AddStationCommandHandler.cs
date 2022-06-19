using Application.Mediator.Command;
using Domain.Common.Interfaces;
using MediatR;

namespace ApplicationQlApp.Mediator;

public class AddStationCommandHandler : INotificationHandler<AddStationCommand>
{
    private readonly IStationRepository _stationRepository;

    public AddStationCommandHandler(IStationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    public async Task Handle(AddStationCommand notification, CancellationToken cancellationToken)
    {
        
        Console.WriteLine($" ====== Kafka consumer start: {notification.NewStation.StationName}");

        // foreach (var station in await _giosService.GetAllStations())
        // {
        //     var find = await _stationRepository.GetStationByIdentifierAsync(station.Id);
        //     if (find is null)
        //     {
        //         await _stationRepository.AddStationAsync(station.ToEntityStation());
        //         continue;
        //     }
        //
        //     find
        //         .SetAddressStreet(station.AddressStreet)
        //         .SetCityName(station.City?.Name ?? string.Empty)
        //         .SetDistrictName(station.City?.Commune?.DistrictName ?? string.Empty)
        //         .SetGegrLat(station.GegrLat)
        //         .SetGegrLon(station.GegrLon)
        //         .SetProvinceName(station.City?.Commune?.ProvinceName ?? string.Empty)
        //         .SetStationName(station.StationName);
        // }
        //
        // await _stationRepository.SaveChangesAsync(cancellationToken);
    }
}