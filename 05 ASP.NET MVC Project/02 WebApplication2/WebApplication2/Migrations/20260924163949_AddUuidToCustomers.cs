using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class AddUuidToCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "CONVERT(nvarchar(36), NEWID())");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Uuid",
                table: "Customers",
                column: "Uuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Uuid",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Customers");
        }
    }
}