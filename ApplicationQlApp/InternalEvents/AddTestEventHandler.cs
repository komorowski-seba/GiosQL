using Domain.Events;
using MediatR;

namespace ApplicationQlApp.InternalEvents;

public class AddTestEventHandler : INotificationHandler<AddTestEvent>
{
    public async Task Handle(AddTestEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($" ######### >>>> add test: station Id:{notification.Id} | station identfy: {notification.StationIdentifier}");
    }
}