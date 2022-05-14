using Application.Models.GiosStation;
using MediatR;

namespace Application.Mediator;

public class AddStationCommand : INotification
{
    public Station NewStation { get; set; }
}