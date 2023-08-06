using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelDTO_Packages_PackagesPackageId",
                table: "HotelDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristAttractionDTO_Packages_PackagesPackageId",
                table: "TouristAttractionDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristAttractionDTO",
                table: "TouristAttractionDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelDTO",
                table: "HotelDTO");

            migrationBuilder.RenameTable(
                name: "TouristAttractionDTO",
                newName: "touristAttractionDTOs");

            migrationBuilder.RenameTable(
                name: "HotelDTO",
                newName: "hotelDTOs");

            migrationBuilder.RenameIndex(
                name: "IX_TouristAttractionDTO_PackagesPackageId",
                table: "touristAttractionDTOs",
                newName: "IX_touristAttractionDTOs_PackagesPackageId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelDTO_PackagesPackageId",
                table: "hotelDTOs",
                newName: "IX_hotelDTOs_PackagesPackageId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Packages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Packages",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Packages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_touristAttractionDTOs",
                table: "touristAttractionDTOs",
                column: "TouristAttractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hotelDTOs",
                table: "hotelDTOs",
                column: "HotelId");

            migrationBuilder.CreateTable(
                name: "ImageGalleries",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGalleries", x => x.ImageId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_hotelDTOs_Packages_PackagesPackageId",
                table: "hotelDTOs",
                column: "PackagesPackageId",
                principalTable: "Packages",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_touristAttractionDTOs_Packages_PackagesPackageId",
                table: "touristAttractionDTOs",
                column: "PackagesPackageId",
                principalTable: "Packages",
                principalColumn: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hotelDTOs_Packages_PackagesPackageId",
                table: "hotelDTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_touristAttractionDTOs_Packages_PackagesPackageId",
                table: "touristAttractionDTOs");

            migrationBuilder.DropTable(
                name: "ImageGalleries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_touristAttractionDTOs",
                table: "touristAttractionDTOs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hotelDTOs",
                table: "hotelDTOs");

            migrationBuilder.RenameTable(
                name: "touristAttractionDTOs",
                newName: "TouristAttractionDTO");

            migrationBuilder.RenameTable(
                name: "hotelDTOs",
                newName: "HotelDTO");

            migrationBuilder.RenameIndex(
                name: "IX_touristAttractionDTOs_PackagesPackageId",
                table: "TouristAttractionDTO",
                newName: "IX_TouristAttractionDTO_PackagesPackageId");

            migrationBuilder.RenameIndex(
                name: "IX_hotelDTOs_PackagesPackageId",
                table: "HotelDTO",
                newName: "IX_HotelDTO_PackagesPackageId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Packages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Packages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Packages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristAttractionDTO",
                table: "TouristAttractionDTO",
                column: "TouristAttractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelDTO",
                table: "HotelDTO",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelDTO_Packages_PackagesPackageId",
                table: "HotelDTO",
                column: "PackagesPackageId",
                principalTable: "Packages",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristAttractionDTO_Packages_PackagesPackageId",
                table: "TouristAttractionDTO",
                column: "PackagesPackageId",
                principalTable: "Packages",
                principalColumn: "PackageId");
        }
    }
}
