using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Vorhangschloss",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Profil_Knaufzylinder",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Profil_Halbzylinder",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Profil_Doppelzylinder",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Hebelzylinder",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Aussenzylinder_Rundzylinder",
                newName: "Price");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Vorhangschloss",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Profil_Knaufzylinder",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Profil_Halbzylinder",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Profil_Doppelzylinder",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Hebelzylinder",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Aussenzylinder_Rundzylinder",
                newName: "Cost");

          
        }
    }
}
