using Application.Models.GiosStation;
using MediatR;

namespace Application.Mediator.Command;

public class AddStationCommand : INotification
{
    public Station NewStation { get; set; }
}