using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgencyMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                });

            migrationBuilder.CreateTable(
                name: "TravelAgents",
                columns: table => new
                {
                    AgentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentStatus = table.Column<bool>(type: "bit", nullable: false),
                    AgentRegistrationDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelAgents", x => x.AgentId);
                });

            migrationBuilder.CreateTable(
                name: "HotelDTO",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackagesPackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelDTO", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_HotelDTO_Packages_PackagesPackageId",
                        column: x => x.PackagesPackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "TouristAttractionDTO",
                columns: table => new
                {
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristAttractionName = table.Column<int>(type: "int", nullable: true),
                    PackagesPackageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttractionDTO", x => x.TouristAttractionId);
                    table.ForeignKey(
                        name: "FK_TouristAttractionDTO_Packages_PackagesPackageId",
                        column: x => x.PackagesPackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "TourOffers",
                columns: table => new
                {
                    TourOfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourOffers", x => x.TourOfferId);
                    table.ForeignKey(
                        name: "FK_TourOffers_TravelAgents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "TravelAgents",
                        principalColumn: "AgentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelDTO_PackagesPackageId",
                table: "HotelDTO",
                column: "PackagesPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttractionDTO_PackagesPackageId",
                table: "TouristAttractionDTO",
                column: "PackagesPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TourOffers_AgentId",
                table: "TourOffers",
                column: "AgentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelDTO");

            migrationBuilder.DropTable(
                name: "TouristAttractionDTO");

            migrationBuilder.DropTable(
                name: "TourOffers");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "TravelAgents");
        }
    }
}
