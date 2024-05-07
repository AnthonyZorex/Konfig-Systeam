using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Artikelnummer",
                table: "Orders");

            migrationBuilder.AddColumn<float>(
                name: "KeyPrice",
                table: "Profil_Doppelzylinder",
                type: "real",
                nullable: true);

           
        }

       
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "KeyPrice",
                table: "Profil_Doppelzylinder");

            migrationBuilder.AddColumn<string>(
                name: "Artikelnummer",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

        }
    }
}
