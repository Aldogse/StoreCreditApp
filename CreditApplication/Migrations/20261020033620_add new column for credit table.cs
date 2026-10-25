using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditApplication.Migrations
{
    /// <inheritdoc />
    public partial class addnewcolumnforcredittable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Credits",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Credits");
        }
    }
}
