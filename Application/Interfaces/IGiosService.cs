using Application.Models.GiosStation;

namespace Application.Interfaces
{
    public interface IGiosService
    {
        Task<IList<Station>?> GetAllStations();
        // IList<ProvinceDto> MapToProvinces(IList<Station> stations);
        // Task<IndexAirQuality> GetStationAirQuality(long stationId, string provinceName, string cityName);
    }
}