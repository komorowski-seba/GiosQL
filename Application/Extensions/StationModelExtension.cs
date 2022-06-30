using Application.Models.Cache;
using Domain.Entities;

namespace Application.Extensions;

public static class StationModelExtension
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