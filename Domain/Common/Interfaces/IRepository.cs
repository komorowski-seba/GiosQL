namespace Domain.Common.Interfaces
{
    public interface IRepository<T> where T : IAggregate
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}