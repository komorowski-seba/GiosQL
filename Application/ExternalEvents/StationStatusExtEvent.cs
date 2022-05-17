using Application.Interfaces;
using Application.Models.GiosStation;

namespace Application.ExternalEvents;

public class StationStatusExtEvent : IExternalEvent
{
    public Guid Id { get; set; }
    public IndexAirQuality? Status { get; set; } 
}