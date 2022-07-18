using System.Text;
using Application.Extensions;
using Application.Interfaces;
using Application.Models.GiosStation;
using Application.Models.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Redis;

public class RedisCacheService : ICacheService
{
    private const string VariableKey = "stations"; // todo extension conf
    private readonly IDistributedCache _distributedCache;

    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task CacheStations(IEnumerable<Station> stations, CancellationToken cancellationToken)
    {
        var stationsCase = stations.Select(n => n.ToStationCache()).ToList();
        var allStationCase = await GetAllStations(cancellationToken);
        var difference = stationsCase.Except(allStationCase).ToList();
        
        if (difference.Any())
            allStationCase.AddRange(difference);
        
        var stationSerialize = JsonConvert.SerializeObject(allStationCase);
        var stationToByte = Encoding.ASCII.GetBytes(stationSerialize);
        await _distributedCache.SetAsync(VariableKey, stationToByte, cancellationToken);
    }

    public async Task<List<StationCache>> GetAllStations(CancellationToken cancellationToken)
    {
        var stationsToByte = await _distributedCache.GetAsync(VariableKey, cancellationToken);
        if (stationsToByte is null)
            return new List<StationCache>();

        var result = JsonConvert.DeserializeObject<List<StationCache>>(Encoding.ASCII.GetString(stationsToByte));
        return result ?? new List<StationCache>();
    }
}