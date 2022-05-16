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

    public async Task<IndexAirQuality?> GetStationAirQuality(long stationId)
    {
        var client = new RestClient($"https://api.gios.gov.pl/pjp-api/rest/aqindex/getIndex/{stationId}"); //($"{_appsettings.GiosStation.Quality}/{stationId}");
        var request = new RestRequest(Method.GET);
        var response = client.Get(request);
        if (!response.IsSuccessful)
            throw new HttpRequestException(response.ErrorException?.Message ?? string.Empty);
                 
        var result = JsonConvert.DeserializeObject<IndexAirQuality>(response.Content);
        return result;
    }
}