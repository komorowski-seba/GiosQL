using Application.ExternalEvents;
using Application.Interfaces;
using Application.Mediator.Command;
using MediatR;

namespace ApplicationHangfire.Mediator;

public class NewStationHandler : INotificationHandler<NewStationCommand>
{
    private readonly IGiosService _giosService;
    private readonly IExternalEventService<NewStationExtEvent> _externalEventService;
    private readonly ICacheService _cacheService;

    public NewStationHandler(
        IGiosService giosService, 
        IExternalEventService<NewStationExtEvent> externalEventService, 
        ICacheService cacheService)
    {
        _giosService = giosService;
        _externalEventService = externalEventService;
        _cacheService = cacheService;
    }

    public async Task Handle(NewStationCommand notification, CancellationToken cancellationToken)
    {
        var allStations = await _giosService.GetAllStations();
        if (allStations is null)
            return;

        await _cacheService.CacheStations(allStations, cancellationToken);
        // foreach (var station in allStations)
        // {
        //     await _externalEventService.Publish(new NewStationExtEvent {NewStation = station});
        // }
    }
}