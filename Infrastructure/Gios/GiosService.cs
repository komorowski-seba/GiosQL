using Application.Interfaces;
using Application.Models.GiosStation;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.Gios;

public class GiosService : IGiosService
{
    // private readonly IAppsettingsConfigServices _appsettings;

    public GiosService(/*IAppsettingsConfigServices appsettings*/)
    {
        // _appsettings = appsettings;
    }
    
    public Task<IList<Station>?> GetAllStations()
    {
        var client = new RestClient("https://api.gios.gov.pl/pjp-api/rest/station/findAll"); //_appsettings.GiosStation.Stations);
        var request = new RestRequest(Method.GET);
        var response = client.Get(request);
        
        if (!response.IsSuccessful)
            throw new HttpRequestException(response.ErrorException?.Message ?? string.Empty);
            
        var stations = JsonConvert.DeserializeObject<IList<Station>>(response.Content);
        return Task.FromResult(stations);
    }

    // public IList<ProvinceDto> MapToProvinces(IList<Station> stations)
    // {
    //     var provincesNew = new Dictionary<string, ProvinceDto>();
    //     foreach (var station in stations)
    //     {
    //         var city = station.City;
    //         var commune = city.Commune;
    //
    //         if (!provincesNew.TryGetValue(commune.ProvinceName, out var province))
    //         {
    //             province = new ProvinceDto(commune.ProvinceName);
    //             provincesNew.Add(commune.ProvinceName, province);
    //         }
    //         
    //         province
    //             .AddCommune(commune.DistrictName, commune.DistrictName)
    //             .AddCity(city.Id, city.Name)
    //             .AddStation(station.Id, station.StationName, station.GegrLat, station.GegrLon, station.AddressStreet);
    //     }
    //     return provincesNew.Values.ToList();
    // }

    // public async Task<IndexAirQuality> GetStationAirQuality(long stationId, string provinceName, string cityName)
    // {
    //     var client = new RestClient($"{_appsettings.GiosStation.Quality}/{stationId}");
    //     var request = new RestRequest(Method.GET);
    //     var response = client.Get(request);
    //     if (!response.IsSuccessful)
    //         throw new ResponseException($"[{_appsettings.GiosStation.Quality}] can't respond: {response.ErrorException.Message}");
    //         
    //     var result = JsonConvert.DeserializeObject<IndexAirQuality>(response.Content);
    //     result.CityName = cityName;
    //     result.ProvinceName = provinceName;
    //     return result;
    // }
}