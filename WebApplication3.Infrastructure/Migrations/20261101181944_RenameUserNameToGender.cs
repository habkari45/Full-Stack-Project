using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserNameToGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "Name");
        }
    }
}
