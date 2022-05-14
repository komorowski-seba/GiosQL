using Domain.Common.Interfaces;
using Domain.Events;

namespace Domain.StorageEvents;

public class StationStorageEvent
{
    public Guid Id { get; private set; }
    public IEvent Description { get; private set; }

    public void Apply(AddStationEvent evt)
    {
        Description = evt;
    }
}