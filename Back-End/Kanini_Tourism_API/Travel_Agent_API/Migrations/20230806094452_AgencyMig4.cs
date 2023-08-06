using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgencyMig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TouristAttractionName",
                table: "TouristAttractionDTO");

            migrationBuilder.DropColumn(
                name: "HotelName",
                table: "HotelDTO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TouristAttractionName",
                table: "TouristAttractionDTO",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelName",
                table: "HotelDTO",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
