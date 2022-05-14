using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Station> Stations { get; init; }
        DbSet<AirTest> AirTests { get; init; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}