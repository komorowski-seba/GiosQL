using Application.ExternalEvents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalEvents;

public class StationStatusEventHandler : INotificationHandler<StationStatusExtEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public StationStatusEventHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task Handle(StationStatusExtEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($" === >>>> [{notification.Status.Id}]");
    }
}