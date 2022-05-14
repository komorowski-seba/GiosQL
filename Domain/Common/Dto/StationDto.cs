namespace Domain.Common.Dto;

public class StationDto
{
    public long Identifier { get; set; }
    public string? StationName { get; set; }
    public string? GegrLat { get; set; }
    public string? GegrLon { get; set; }
    public string? AddressStreet { get; set; }
    public string? CityName { get; set; }
    public string? DistrictName { get; set; }
    public string? ProvinceName { get; set; }
}