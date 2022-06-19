using Application.ExternalEvents;
using Domain.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationQlApp.ExternalEvents;

public class StationStatusEventHandler : INotificationHandler<StationStatusExtEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<StationStatusEventHandler> _logger;

    public StationStatusEventHandler(IServiceScopeFactory scopeFactory, ILogger<StationStatusEventHandler> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task Handle(StationStatusExtEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Status is null)
            return;
        
        var repo = _scopeFactory
            .CreateScope()
            .ServiceProvider
            .GetService<IStationRepository>();

        if (repo is null)
            throw new NullReferenceException(nameof(IStationRepository));

        var findStation = await repo.GetStationByIdentifierAsync(notification.Status.Id, cancellationToken);
        if (findStation is null)
        {
            _logger.LogWarning(@"station id: {Id}", notification.Status.Id);
            return;
        }

        findStation.AddTest(
            notification.Status.CalculateDate.UtcDateTime,
            DateTime.UtcNow,
            notification.Status.So2IndexLevel?.Value ?? -1,
            notification.Status.So2IndexLevel?.IndexLevelName ?? "",
            notification.Status.No2IndexLevel?.Value ?? -1,
            notification.Status.No2IndexLevel?.IndexLevelName ?? "",
            notification.Status.Pm10IndexLevel?.Value ?? -1,
            notification.Status.Pm10IndexLevel?.IndexLevelName ?? "",
            notification.Status.Pm25IndexLevel?.Value ?? -1,
            notification.Status.Pm25IndexLevel?.IndexLevelName ?? "",
            notification.Status.O3IndexLevel?.Value ?? -1,
            notification.Status.O3IndexLevel?.IndexLevelName ?? "");
        await repo.SaveChangesAsync(cancellationToken);
    }
}