using Application.Interfaces;
using Application.Mediator.Command;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Hangfire;

public class HangfireJobsService : IHangfireJobsService
{
    private readonly IMediator _mediator;
    private readonly ILogger<HangfireJobsService> _logger;

    public HangfireJobsService(IMediator mediator, ILogger<HangfireJobsService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [JobDisplayName("AllStationsJob_{0}"), AutomaticRetry(Attempts = 0)]
    public void AllStationJob()
    {
        // _mediator.Publish(new NewStationCommand()).Wait();
        _logger.LogInformation(" ### hej >>>");
    }

    [JobDisplayName("AllStationsStatus_{0}"), AutomaticRetry(Attempts = 0)]
    public void AllStationsStatusJob()
    {
        // _mediator.Publish(new CheckStationStatusCommand()).Wait();
    }
}