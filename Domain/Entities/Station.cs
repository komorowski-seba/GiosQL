using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Events;

namespace Domain.Entities;

public class Station : Aggregate, IEntities
{
    public long Identifier { get; private set; }
    public string StationName { get; private set; }
    public string GegrLat { get; private set; }
    public string GegrLon { get; private set; }
    public string AddressStreet { get; private set; }
    public string CityName { get; private set; }
    public string DistrictName { get; private set; }
    public string ProvinceName { get; private set; }
    
    private readonly List<AirTest> _tests;
    public IReadOnlyCollection<AirTest> Tests => _tests;

    public static Station Create(
        long identifier,
        string stationName,
        string gegrLat,
        string gegrLon,
        string addressStreet,
        string cityName,
        string districtName,
        string provinceName)
        => new Station(identifier, stationName, gegrLat, gegrLon, addressStreet, cityName, districtName, provinceName);
    
    private Station() { }
    
    private Station(
        long identifier, 
        string stationName, 
        string gegrLat, 
        string gegrLon, 
        string addressStreet, 
        string cityName,
        string districtName,
        string provinceName)
    {
        if (string.IsNullOrEmpty(stationName))
            throw new ArgumentNullException(nameof(stationName));
        if (string.IsNullOrEmpty(gegrLat))
            throw new ArgumentNullException(nameof(gegrLat));
        if (string.IsNullOrEmpty(gegrLon))
            throw new ArgumentNullException(nameof(gegrLon));
        if (string.IsNullOrEmpty(cityName))
            throw new ArgumentNullException(nameof(cityName));
        if (string.IsNullOrEmpty(districtName))
            throw new ArgumentNullException(nameof(districtName));
        if (string.IsNullOrEmpty(provinceName))
            throw new ArgumentNullException(nameof(provinceName));

        Id = Guid.NewGuid();
        Identifier = identifier;
        StationName = stationName;
        GegrLat = gegrLat;
        GegrLon = gegrLon;
        AddressStreet = addressStreet ?? "";
        CityName = cityName;
        DistrictName = districtName;
        ProvinceName = provinceName;
        
        AddEvent(new AddStationEvent(
            Id,
            identifier, 
            stationName, 
            gegrLat, 
            gegrLon, 
            addressStreet, 
            cityName,
            districtName,
            provinceName));
    }

    public void AddTest(
        DateTime calcDate,
        DateTime downloadDate,
        int so2IndexLevel,
        string so2IndexName,
        int no2IndexLevel,
        string no2IndexName,
        int pm10IndexLevel,
        string pm10IndexName,
        int pm25IndexLevel,
        string pm25IndexName,
        int o3IndexLevel,
        string o3IndexName)
    {
        _tests.Add(AirTest.Create(
            calcDate, 
            downloadDate, 
            so2IndexLevel, 
            so2IndexName, 
            no2IndexLevel,
            no2IndexName,
            pm10IndexLevel,
            pm10IndexName,
            pm25IndexLevel,
            pm25IndexName,
            o3IndexLevel,
            o3IndexName,
            this));
        
        AddEvent(new AddTestEvent(
            Identifier,
            calcDate,
            downloadDate,
            so2IndexLevel,
            so2IndexName,
            no2IndexLevel,
            no2IndexName,
            pm10IndexLevel,
            pm10IndexName,
            pm25IndexLevel,
            pm25IndexName,
            o3IndexLevel,
            o3IndexName));
    }

    public Station SetStationName(string value)
    {
        if (string.IsNullOrEmpty(value) || StationName.Equals(value))
            return this;

        ++Version;
        StationName = value;
        AddEvent(new SetNameStationEvent(value, Id));
        return this;
    }

    public Station SetGegrLat(string value)
    {
        if (string.IsNullOrEmpty(value) || GegrLat.Equals(value))
            return this;

        ++Version;
        GegrLat = value;
        AddEvent(new SetGegrLatStationEvent(value, Id));
        return this;
    }

    public Station SetGegrLon(string value)
    {
        if (string.IsNullOrEmpty(value) || GegrLon.Equals(value))
            return this;

        ++Version;
        GegrLon = value;
        AddEvent(new SetGegrLonStationEvent(value, Id));
        return this;
    }

    public Station SetAddressStreet(string value)
    {
        if (string.IsNullOrEmpty(value) || AddressStreet.Equals(value))
            return this;
        
        ++Version;
        AddressStreet = value;
        AddEvent(new SetAddressStreetStationEvent(Id, value));
        return this;
    }

    public Station SetCityName(string value)
    {
        if (string.IsNullOrEmpty(value) || CityName.Equals(value))
            return this;
        
        ++Version;
        CityName = value;
        AddEvent(new SetCityNameStationEvent(Id, value));
        return this;
    }

    public Station SetDistrictName(string value)
    {
        if (string.IsNullOrEmpty(value) || DistrictName.Equals(value))
            return this;
        
        ++Version;
        DistrictName = value;
        AddEvent(new SetDistrictNameStationEvent(value, Id));
        return this;
    }

    public Station SetProvinceName(string value)
    {
        if (string.IsNullOrEmpty(value) || ProvinceName.Equals(value))
            return this;
        
        ++Version;
        ProvinceName = value;
        AddEvent(new SetProvinceNameStationEvent(value, Id));
        return this;
    }
}