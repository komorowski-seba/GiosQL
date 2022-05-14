using Domain.Common.Interfaces;

namespace Domain.Common;

public abstract class Aggregate : IAggregate
{
    public Guid Id { get; protected init; }
    public int Version { get; protected set; }
    public DateTime CreateUtc { get; protected set;  }

    [NonSerialized]
    private readonly List<IEvent> _events = new();

    public IEnumerable<IEvent> UncommittedEvents
    {
        get
        {
            var result = _events.ToArray();
            _events.Clear();
            return result;
        }
    }

    public void AddEvent(IEvent evt)
    {
        ++Version;
        CreateUtc = DateTime.UtcNow;
        _events.Add(evt);
    }
}