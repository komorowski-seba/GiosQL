using Application.Interfaces;
using Application.Mediator.Command;
using Hangfire;
using MediatR;

namespace HangfireInfrastructure;

public class HangfireJobsService : IHangfireJobsService
{
    private readonly IMediator _mediator;

    public HangfireJobsService(IMediator mediator)
    {
        _mediator = mediator;
    }

    [JobDisplayName("AllStationsJob_{0}"), AutomaticRetry(Attempts = 0)]
    public void AllStationJob()
    {
        // _mediator.Publish(new NewStationCommand()).Wait();
    }

    [JobDisplayName("AllStationsStatus_{0}"), AutomaticRetry(Attempts = 0)]
    public void AllStationsStatusJob()
    {
        // _mediator.Publish(new CheckStationStatusCommand()).Wait();
    }
}