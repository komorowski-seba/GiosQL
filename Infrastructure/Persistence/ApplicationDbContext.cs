using System.Reflection;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly IEventsStoreDb _eventsStoreDb;
    
    public DbSet<Station> Stations { get; init; }
    public DbSet<AirTest> AirTests { get; init; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator, 
        IEventsStoreDb eventsStoreDb) 
        : base(options)
    {
        _mediator = mediator;
        _eventsStoreDb = eventsStoreDb;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not Aggregate aggregate)
                continue;

            foreach (var currentEvt in aggregate.UncommittedEvents)
            {
                await _eventsStoreDb.AppendEventAsync(currentEvt, cancellationToken);
                await _mediator.Publish(currentEvt, cancellationToken);
            }
        }
        
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}