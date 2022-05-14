using Domain.Entities;

namespace Domain.Common.Interfaces;

public interface IStationRepository : IRepository<Station>
{
    Task<Station?> GetStationByIdentifierAsync(long identifier, CancellationToken cancellationToken);
    Task<List<Station>> GetListAsync(CancellationToken cancellationToken);
    Task AddStationAsync(Station station);
}