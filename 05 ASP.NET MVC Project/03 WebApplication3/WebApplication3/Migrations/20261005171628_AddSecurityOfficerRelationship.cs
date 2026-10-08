using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class AddSecurityOfficerRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecurityOfficerId",
                table: "SecurityIncidents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityIncidents_SecurityOfficerId",
                table: "SecurityIncidents",
                column: "SecurityOfficerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityIncidents_SecurityOfficers_SecurityOfficerId",
                table: "SecurityIncidents",
                column: "SecurityOfficerId",
                principalTable: "SecurityOfficers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecurityIncidents_SecurityOfficers_SecurityOfficerId",
                table: "SecurityIncidents");

            migrationBuilder.DropIndex(
                name: "IX_SecurityIncidents_SecurityOfficerId",
                table: "SecurityIncidents");

            migrationBuilder.DropColumn(
                name: "SecurityOfficerId",
                table: "SecurityIncidents");
        }
    }
}
