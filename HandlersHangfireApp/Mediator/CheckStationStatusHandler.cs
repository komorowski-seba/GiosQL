using Application.ExternalEvents;
using Application.Interfaces;
using Application.Mediator.Command;
using MediatR;

namespace HandlersHangfireApp.Mediator;

public class CheckStationStatusHandler : INotificationHandler<CheckStationStatusCommand>
{
    private readonly IGiosService _giosService;
    private readonly IExternalEventService<StationStatusExtEvent> _externalEventService;
    private readonly ICacheService _cacheService;

    public CheckStationStatusHandler(
        IGiosService giosService, 
        ICacheService cacheService, 
        IExternalEventService<StationStatusExtEvent> externalEventService)
    {
        _giosService = giosService;
        _cacheService = cacheService;
        _externalEventService = externalEventService;
    }

    public async Task Handle(CheckStationStatusCommand notification, CancellationToken cancellationToken)
    {
        var allStations = await _cacheService.GetAllStations(cancellationToken);
        foreach (var station in allStations)
        {
            var currentState = await _giosService.GetStationAirQuality(station.Id);
            if (currentState is null)
                continue;
            
            await _externalEventService.Publish(new StationStatusExtEvent {Status = currentState});
        }
    }
}