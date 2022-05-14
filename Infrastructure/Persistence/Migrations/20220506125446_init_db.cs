using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class init_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<long>(type: "bigint", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GegrLat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GegrLon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreateUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationIdentifier = table.Column<long>(type: "bigint", nullable: false),
                    CalcDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DownloadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    So2IndexLevel = table.Column<int>(type: "int", nullable: false),
                    So2IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    No2IndexLevel = table.Column<int>(type: "int", nullable: false),
                    No2IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pm10IndexLevel = table.Column<int>(type: "int", nullable: false),
                    Pm10IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pm25IndexLevel = table.Column<int>(type: "int", nullable: false),
                    Pm25IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    O3IndexLevel = table.Column<int>(type: "int", nullable: false),
                    O3IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirTests_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirTests_StationId",
                table: "AirTests",
                column: "StationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirTests");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
