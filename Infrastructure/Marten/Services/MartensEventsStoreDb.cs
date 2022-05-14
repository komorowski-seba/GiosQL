using Application.Interfaces;
using Domain.Common.Interfaces;
using Marten;

namespace Infrastructure.Marten.Services;

public class MartensEventsStoreDb : IEventsStoreDb
{
    private readonly IDocumentSession _documentSession;

    public MartensEventsStoreDb(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task AppendEventAsync(IEvent evt, CancellationToken cancellationToken)
    {
        _documentSession.Events.Append(evt.Id, evt);
        await _documentSession.SaveChangesAsync(cancellationToken);
    }
}