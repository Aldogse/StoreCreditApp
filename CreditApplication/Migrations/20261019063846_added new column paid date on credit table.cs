using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditApplication.Migrations
{
    /// <inheritdoc />
    public partial class addednewcolumnpaiddateoncredittable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "Credits",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "Credits");
        }
    }
}
