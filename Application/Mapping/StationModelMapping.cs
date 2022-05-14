using Application.Models.Redis;
using Domain.Common.Dto;
using Domain.Entities;

namespace Application.Mapping;

public static class StationModelMapping
{
    public static Station ToEntityStation(this Models.GiosStation.Station station)
    {
        var result = Station.Create(
            station.Id,
            station.StationName,
            station.GegrLat,
            station.GegrLon,
            station.AddressStreet,
            station.City?.Name ?? "[NULL]",
            station.City?.Commune?.DistrictName ?? "[NULL]",
            station.City?.Commune?.ProvinceName ?? "[NULL]");
        return result;
    }

    public static StationCache ToStationCache(this Models.GiosStation.Station station)
    {
        var result = new StationCache
        {
            Id = station.Id,
            GegrLat = station.GegrLat,
            GegrLon = station.GegrLon,
            StationName = station.StationName,
            City = station.City?.Name ?? string.Empty,
            Province = station.City?.Commune?.ProvinceName ?? string.Empty 
        };
        return result;
    }
}

public static class StationMaping
{
    public static StationDto ToStationDto(this Station station)
    {
        var result = new StationDto
        {
            Identifier = station.Identifier,
            AddressStreet = station.AddressStreet,
            DistrictName = station.DistrictName,
            ProvinceName = station.ProvinceName,
            StationName = station.StationName,
            GegrLat = station.GegrLat,
            GegrLon = station.GegrLon,
            CityName = station.CityName
        };
        return result;
    }
}