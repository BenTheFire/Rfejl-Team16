using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketmaster.Migrations
{
    /// <inheritdoc />
    public partial class lastofyou : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ByVendorId",
                table: "Locations",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ByVendorId",
                table: "Locations",
                column: "ByVendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_ByVendorId",
                table: "Locations",
                column: "ByVendorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_ByVendorId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_ByVendorId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ByVendorId",
                table: "Locations");
        }
    }
}
