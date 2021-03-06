// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220506125446_init_db")]
    partial class init_db
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.AirTest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CalcDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DownloadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("No2IndexLevel")
                        .HasColumnType("int");

                    b.Property<string>("No2IndexName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("O3IndexLevel")
                        .HasColumnType("int");

                    b.Property<string>("O3IndexName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pm10IndexLevel")
                        .HasColumnType("int");

                    b.Property<string>("Pm10IndexName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pm25IndexLevel")
                        .HasColumnType("int");

                    b.Property<string>("Pm25IndexName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("So2IndexLevel")
                        .HasColumnType("int");

                    b.Property<string>("So2IndexName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("StationIdentifier")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StationId");

                    b.ToTable("AirTests");
                });

            modelBuilder.Entity("Domain.Entities.Station", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressStreet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GegrLat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GegrLon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Identifier")
                        .HasColumnType("bigint");

                    b.Property<string>("ProvinceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("Domain.Entities.AirTest", b =>
                {
                    b.HasOne("Domain.Entities.Station", "Station")
                        .WithMany("Tests")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Station");
                });

            modelBuilder.Entity("Domain.Entities.Station", b =>
                {
                    b.Navigation("Tests");
                });
#pragma warning restore 612, 618
        }
    }
}
