using Domain.Common.Interfaces;

namespace Domain.Events;

public class SetGegrLonStationEvent : IEvent
{
    public Guid Id { get; }
    public string GegrLon { get; }
    
    public SetGegrLonStationEvent(string gegrLon, Guid stationId)
    {
        GegrLon = gegrLon;
        Id = stationId;
    }
}