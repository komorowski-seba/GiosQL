using Newtonsoft.Json;

namespace Application.Models.Cache;

public class StationCache : IEquatable<StationCache>
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("gegrLat")] public string? GegrLat { get; set; }
    [JsonProperty("gegrLon")] public string? GegrLon { get; set; }
    [JsonProperty("stationName")] public string? StationName { get; set; }
    [JsonProperty("cityName")] public string? City { get; set; }
    [JsonProperty("provinceName")] public string? Province { get; set; }

    public bool Equals(StationCache? cache)
    {
        if (cache is null)
            return false;

        return Id == cache.Id;
    }

    public override bool Equals(object? obj)
        => Equals(obj as StationCache);

    public override int GetHashCode()
    {
        return (int)Id;
    }
}