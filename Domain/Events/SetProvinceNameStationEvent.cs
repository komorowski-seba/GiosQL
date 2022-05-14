using Domain.Common.Interfaces;

namespace Domain.Events;

public class SetProvinceNameStationEvent : IEvent
{
    public Guid Id { get; }
    public string ProvinceName { get; }
    
    public SetProvinceNameStationEvent(string provinceName, Guid stationId)
    {
        ProvinceName = provinceName;
        Id = stationId;
    }
}