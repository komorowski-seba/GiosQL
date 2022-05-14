using Application.Interfaces;
using Application.Models.GiosStation;

namespace Application.ExternalEvents;

public class NewStationExtEvent : IExternalEvent
{
    public Guid Id { get; set; }
    public Station? NewStation { get; set; }
}