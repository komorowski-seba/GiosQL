using Domain.Common.Interfaces;

namespace Domain.Events;

public class SetGegrLatStationEvent : IEvent
{
    public Guid Id { get; }
    public string GegrLat { get; }
    
    public SetGegrLatStationEvent(string gegrLat, Guid stationId)
    {
        GegrLat = gegrLat;
        Id = stationId;
    }
}