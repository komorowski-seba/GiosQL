using Application.Models.GiosStation;
using Application.Models.Redis;

namespace Application.Interfaces;

public interface ICacheService
{
    Task CacheStations(IEnumerable<Station> stations, CancellationToken cancellationToken);
    Task<List<StationCache>> GetAllStations(CancellationToken cancellationToken);
}