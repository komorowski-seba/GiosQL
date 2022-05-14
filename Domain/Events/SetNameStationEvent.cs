using Domain.Common.Interfaces;

namespace Domain.Events;

public class SetNameStationEvent : IEvent
{
    public Guid Id { get; }
    public string StationName { get; }
    
    public SetNameStationEvent(string stationName, Guid stationId)
    {
        StationName = stationName;
        Id = stationId;
    }
}