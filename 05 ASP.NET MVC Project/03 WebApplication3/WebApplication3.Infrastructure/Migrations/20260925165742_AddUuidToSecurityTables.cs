using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUuidToSecurityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "SecurityOfficers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "CONVERT(nvarchar(36), NEWID())");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "SecurityIncidents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "CONVERT(nvarchar(36), NEWID())");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityOfficers_Uuid",
                table: "SecurityOfficers",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityIncidents_Uuid",
                table: "SecurityIncidents",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SecurityOfficers_Uuid",
                table: "SecurityOfficers");

            migrationBuilder.DropIndex(
                name: "IX_SecurityIncidents_Uuid",
                table: "SecurityIncidents");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "SecurityOfficers");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "SecurityIncidents");
        }
    }
}
