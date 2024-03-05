using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class orderFixssf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Turname",
                table: "isOpen_value",
                newName: "DorName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DorName",
                table: "isOpen_value",
                newName: "Turname");
        }
    }
}
