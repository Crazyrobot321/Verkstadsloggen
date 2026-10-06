using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Verkstadsloggen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicensePlates",
                table: "Customers",
                newName: "LicensePlate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicensePlate",
                table: "Customers",
                newName: "LicensePlates");
        }
    }
}
