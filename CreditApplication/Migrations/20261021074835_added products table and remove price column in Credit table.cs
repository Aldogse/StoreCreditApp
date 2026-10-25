using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CreditApplication.Migrations
{
    /// <inheritdoc />
    public partial class addedproductstableandremovepricecolumninCredittable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product",
                table: "Credits");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Credits",
                newName: "ProductId");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credits_ProductId",
                table: "Credits",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credits_Products_ProductId",
                table: "Credits",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credits_Products_ProductId",
                table: "Credits");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Credits_ProductId",
                table: "Credits");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Credits",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "Credits",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
