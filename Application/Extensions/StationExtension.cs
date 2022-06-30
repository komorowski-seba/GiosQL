using Domain.Common.Dto;
using Domain.Entities;

namespace Application.Extensions;

public static class StationExtension
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