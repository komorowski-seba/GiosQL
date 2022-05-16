using Application.Models.GiosStation;

namespace Application.Interfaces;

public interface IGiosService
{
    Task<IList<Station>?> GetAllStations();
    Task<IndexAirQuality?> GetStationAirQuality(long stationId);
}