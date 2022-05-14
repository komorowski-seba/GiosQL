using Application.Interfaces;
using Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Services;

public class StationRepository : IStationRepository
{
    private readonly IApplicationDbContext _applicationDb;

    public StationRepository(IApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }

    public Task<Station?> GetStationByIdentifierAsync(long identifier, CancellationToken cancellationToken)
    {
        var find = _applicationDb
            .Stations
            .FirstOrDefaultAsync(n => n.Identifier.Equals(identifier), cancellationToken);
        return find;
    }

    public Task<List<Station>> GetListAsync(CancellationToken cancellationToken)
    {
        var result = _applicationDb
            .Stations
            .ToListAsync(cancellationToken);
        return result;
    }

    public async Task AddStationAsync(Station station)
    {
        await _applicationDb.Stations.AddAsync(station);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _applicationDb.SaveChangesAsync(cancellationToken);
    }
}