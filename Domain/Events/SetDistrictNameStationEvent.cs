using Domain.Common.Interfaces;

namespace Domain.Events;

public class SetDistrictNameStationEvent : IEvent
{
    public Guid Id { get; }
    public string DistrictName { get; }
    
    public SetDistrictNameStationEvent(string districtName, Guid stationId)
    {
        DistrictName = districtName;
        Id = stationId;
    }
}