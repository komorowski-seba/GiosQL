using Application.Models.GiosStation;

namespace Application.Interfaces;

public interface IAppsettingsConfigServices
{
    public GiosStationSettings GiosStation { get; }
}