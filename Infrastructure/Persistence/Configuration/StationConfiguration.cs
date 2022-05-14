using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class StationConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).IsRequired().ValueGeneratedNever();

        builder
            .Metadata
            .FindNavigation(nameof(Station.Tests))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}