namespace Domain.Common.Interfaces
{
    public interface IAggregate
    {
        Guid Id { get; }
        int Version { get; }
        DateTime CreateUtc { get; }

        IEnumerable<IEvent> UncommittedEvents { get; }
        void AddEvent(IEvent evt);
    }
}