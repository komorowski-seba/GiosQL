using Domain.Common.Dto;

namespace Application.Interfaces;

public interface IStationService
{
    Task<List<StationDto>> GetStationsListAsync(CancellationToken cancellationToken);
    Task<StationDto> GetStationAsync(int identifier, CancellationToken cancellationToken);
}