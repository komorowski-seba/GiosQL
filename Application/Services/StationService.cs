using Application.Extensions;
using Application.Interfaces;
using Domain.Common.Dto;
using Domain.Common.Interfaces;

namespace Application.Services;

public class StationService : IStationService
{
    private readonly IStationRepository _stationRepository;

    public StationService(IStationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    public async Task<List<StationDto>> GetStationsListAsync(CancellationToken cancellationToken)
    {
        var result = await _stationRepository.GetListAsync(cancellationToken);
        return result
            .Select(n => n.ToStationDto())
            .ToList();
    }

    public async Task<StationDto> GetStationAsync(int identifier, CancellationToken cancellationToken)
    {
        var result = await _stationRepository.GetStationByIdentifierAsync(identifier, cancellationToken);
        if (result is null)
            throw new NullReferenceException($"id {identifier}");
        return result.ToStationDto();
    }
}