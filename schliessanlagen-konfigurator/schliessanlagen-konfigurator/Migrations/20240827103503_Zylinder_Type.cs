using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Zylinder_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.DropColumn(
                name: "isGround",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "isGround",
                table: "Profil_Doppelzylinder");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Vorhangschloss",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Profil_Knaufzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Profil_Halbzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Profil_Doppelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Hebelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Aussenzylinder_Rundzylinder",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Vorhangschloss");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Hebelzylinder");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Aussenzylinder_Rundzylinder");

            migrationBuilder.AddColumn<bool>(
                name: "isGround",
                table: "Profil_Knaufzylinder",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isGround",
                table: "Profil_Doppelzylinder",
                type: "bit",
                nullable: true);

           
        }
    }
}
