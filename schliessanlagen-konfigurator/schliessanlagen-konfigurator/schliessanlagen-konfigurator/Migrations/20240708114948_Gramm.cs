using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Gramm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.AddColumn<float>(
                name: "Gramm",
                table: "Vorhangschloss",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Gramm",
                table: "Profil_Knaufzylinder",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Gramm",
                table: "Profil_Halbzylinder",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Gramm",
                table: "Profil_Doppelzylinder",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Gramm",
                table: "Hebelzylinder",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Gramm",
                table: "Aussenzylinder_Rundzylinder",
                type: "real",
                nullable: true);

        }

   
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "Gramm",
                table: "Vorhangschloss");

            migrationBuilder.DropColumn(
                name: "Gramm",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Gramm",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "Gramm",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Gramm",
                table: "Hebelzylinder");

            migrationBuilder.DropColumn(
                name: "Gramm",
                table: "Aussenzylinder_Rundzylinder");

          
        }
    }
}
