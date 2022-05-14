using Domain.Common.Interfaces;

namespace Domain.Events;

public class SetCityNameStationEvent : IEvent
{
    public Guid Id { get; }
    public string CityName { get; }
    
    public SetCityNameStationEvent(Guid id, string cityName)
    {
        Id = id;
        CityName = cityName;
    }
}