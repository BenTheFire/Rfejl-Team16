using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketMaster.Migrations.MySql
{
    /// <inheritdoc />
    public partial class Guh5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseTime",
                table: "Tickets",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseTime",
                table: "Tickets");
        }
    }
}
