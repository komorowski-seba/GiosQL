using System.Text;
using Application.Extensions;
using Application.Interfaces;
using Application.Models.GiosStation;
using Application.Models.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Redis;

public class RedisCacheService : ICacheService
{
    // private const string VariableKey = "stations"; // todo extension conf
    private readonly IDistributedCache _distributedCache;
    private readonly IConfiguration _configuration;

    public RedisCacheService(IDistributedCache distributedCache, IConfiguration configuration)
    {
        _distributedCache = distributedCache;
        _configuration = configuration;
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
        await _distributedCache.SetAsync(_configuration.GetSettingsRedisVariableKey(), stationToByte, cancellationToken);
    }

    public async Task<List<StationCache>> GetAllStations(CancellationToken cancellationToken)
    {
        var stationsToByte = await _distributedCache.GetAsync(_configuration.GetSettingsRedisVariableKey(), cancellationToken);
        if (stationsToByte is null)
            return Enumerable.Empty<StationCache>().ToList();

        var result = JsonConvert.DeserializeObject<List<StationCache>>(Encoding.ASCII.GetString(stationsToByte));
        return result ?? new List<StationCache>();
    }
}