using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class orderFixss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tur",
                table: "isOpen_value");

            migrationBuilder.AddColumn<string>(
                name: "Turname",
                table: "isOpen_value",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Turname",
                table: "isOpen_value");

            migrationBuilder.AddColumn<string>(
                name: "Tur",
                table: "isOpen_value",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
