using Application.Models.GiosStation;
using Application.Models.Cache;

namespace Application.Interfaces;

public interface ICacheService
{
    Task CacheStations(IEnumerable<Station> stations, CancellationToken cancellationToken);
    Task<List<StationCache>> GetAllStations(CancellationToken cancellationToken);
}