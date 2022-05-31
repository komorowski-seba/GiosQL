using Infrastructure.QL.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.QL.Schema;

public class StationApiSchema : GraphQL.Types.Schema
{
    public StationApiSchema(IServiceProvider services) : base(services)
    {
        var stationQuery = services.GetRequiredService<StationQuery>();
        Query = stationQuery;
    }
}