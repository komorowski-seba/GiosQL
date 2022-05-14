using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class AirTestConfiguration : IEntityTypeConfiguration<AirTest>
{
    public void Configure(EntityTypeBuilder<AirTest> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).IsRequired().ValueGeneratedNever();

        builder
            .HasOne(n => n.Station)
            .WithMany(n => n.Tests)
            .HasForeignKey(n => n.StationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}