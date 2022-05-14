using System.Text;
using Application.Interfaces;
using Application.Mapping;
using Application.Models.GiosStation;
using Application.Models.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Redis;

public class RedisCacheService : ICacheService
{
    private const string VariableKey = "stations";
    private readonly IDistributedCache _distributedCache;

    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task CacheStations(IEnumerable<Station> stations)
    {
        var stationsCase = stations.Select(n => n.ToStationCache()).ToList();
        var allStationCase = await GetAllStations();
        var difference = stationsCase.Except(allStationCase).ToList();
        
        if (difference.Any())
            allStationCase.AddRange(difference);
        
        var stationSerialize = JsonConvert.SerializeObject(allStationCase);
        var stationToByte = Encoding.ASCII.GetBytes(stationSerialize);
        await _distributedCache.SetAsync(VariableKey, stationToByte);
    }

    public async Task<List<StationCache>> GetAllStations()
    {
        var stationsToByte = await _distributedCache.GetAsync(VariableKey);
        if (stationsToByte is null)
            return new List<StationCache>();

        var result = JsonConvert.DeserializeObject<List<StationCache>>(Encoding.ASCII.GetString(stationsToByte));
        return result ?? new List<StationCache>();
    }
}