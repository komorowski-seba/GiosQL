using Application.Interfaces;
using GraphQL;
using GraphQL.Types;
using Infrastructure.QL.Types;

namespace Infrastructure.QL.Queries;

public class StationQuery : ObjectGraphType
{
    public StationQuery(IStationService stationService)
    {
        Field<ListGraphType<StationType>>(
            "stations",
            resolve: context => stationService.GetStationsListAsync(CancellationToken.None));
        
        Field<StationType>(
            "station",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"}),
            resolve: c =>
            {
                var id = c.GetArgument<int>("id");
                return stationService.GetStationAsync(id, CancellationToken.None);
            });
    }
}