using Application.Interfaces;
using Application.Mediator.Command;
using MediatR;
using Newtonsoft.Json;

namespace HandlersHangfireApp.Mediator;

public class CheckStationStatusHandler : INotificationHandler<CheckStationStatusCommand>
{
    private readonly IGiosService _giosService;
    // private readonly IExternalEventService<NewStationExtEvent> _externalEventService;
    private readonly ICacheService _cacheService;

    public CheckStationStatusHandler(IGiosService giosService, ICacheService cacheService)
    {
        _giosService = giosService;
        _cacheService = cacheService;
    }

    public async Task Handle(CheckStationStatusCommand notification, CancellationToken cancellationToken)
    {
        var allStations = await _cacheService.GetAllStations(cancellationToken);
        Console.WriteLine($" ==>>> {allStations.Count}");
        foreach (var station in allStations)
        {
            var currentState = await _giosService.GetStationAirQuality(station.Id);
            var i = JsonConvert.SerializeObject(currentState);
            
            Console.WriteLine($" ==== @@@@ >>> {i}");
        }
    }
}