using Domain.Events;
using MediatR;

namespace ApplicationQlApp.InternalEvents;

public class AddStationEventHandler : INotificationHandler<AddStationEvent>
{
    public async Task Handle(AddStationEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($" ===== >>> ADDDED STATION{notification.Identifier} | {notification.StationName}");
    }
}