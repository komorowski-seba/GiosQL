using Domain.Common.Interfaces;

namespace Domain.Events;

public class AddStationEvent : IEvent
{
    public Guid Id { get; }
    public long Identifier { get; }
    public string StationName { get; }
    public string GegrLat { get; }
    public string GegrLon { get; }
    public string AddressStreet { get; }
    public string CityName { get; }
    public string DistrictName { get; }
    public string ProvinceName { get; }
    
    public AddStationEvent(
        Guid id, 
        long identifier, 
        string stationName, 
        string gegrLat, 
        string gegrLon, 
        string addressStreet, 
        string cityName, 
        string districtName, 
        string provinceName)
    {
        Id = id;
        Identifier = identifier;
        StationName = stationName;
        GegrLat = gegrLat;
        GegrLon = gegrLon;
        AddressStreet = addressStreet;
        CityName = cityName;
        DistrictName = districtName;
        ProvinceName = provinceName;
    }
}