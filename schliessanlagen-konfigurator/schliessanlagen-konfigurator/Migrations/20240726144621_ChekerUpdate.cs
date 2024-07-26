using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class ChekerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SystemScheker",
                newName: "doppel");

            migrationBuilder.AddColumn<bool>(
                name: "Aussen",
                table: "SystemScheker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Halb",
                table: "SystemScheker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hebel",
                table: "SystemScheker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Knayf",
                table: "SystemScheker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vorhang",
                table: "SystemScheker",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aussen",
                table: "SystemScheker");

            migrationBuilder.DropColumn(
                name: "Halb",
                table: "SystemScheker");

            migrationBuilder.DropColumn(
                name: "Hebel",
                table: "SystemScheker");

            migrationBuilder.DropColumn(
                name: "Knayf",
                table: "SystemScheker");

            migrationBuilder.DropColumn(
                name: "Vorhang",
                table: "SystemScheker");

            migrationBuilder.RenameColumn(
                name: "doppel",
                table: "SystemScheker",
                newName: "Value");
        }
    }
}
