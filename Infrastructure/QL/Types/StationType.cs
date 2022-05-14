using Domain.Common.Dto;
using GraphQL.Types;

namespace Infrastructure.QL.Types;

public sealed class StationType : ObjectGraphType<StationDto>
{
    public StationType()
    {
        Name = "station";
        Field(n => n.Identifier, false).Description("int id for station");
        Field(n => n.AddressStreet);
        Field(n => n.GegrLon);
        Field(n => n.GegrLat);
        Field(n => n.DistrictName);
        Field(n => n.ProvinceName);
        Field(n => n.StationName);
        Field(n => n.CityName, false);
    }
}