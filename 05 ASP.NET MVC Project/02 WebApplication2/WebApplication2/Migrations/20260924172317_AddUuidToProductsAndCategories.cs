using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class AddUuidToProductsAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "CONVERT(nvarchar(36), NEWID())");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "CONVERT(nvarchar(36), NEWID())");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Uuid",
                table: "Products",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Uuid",
                table: "Categories",
                column: "Uuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Uuid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Uuid",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Categories");
        }
    }
}