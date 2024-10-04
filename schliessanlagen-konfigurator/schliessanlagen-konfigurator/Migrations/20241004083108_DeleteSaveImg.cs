using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSaveImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "SystemOptionInfo");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "ProductGalery");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "NGF");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Knayf_Options");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Halbzylinder_Options");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Aussenzylinder_Rundzylinder");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Aussen_Rund_all");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "SystemOptionInfo",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Profil_Knaufzylinder",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Profil_Halbzylinder",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Profil_Doppelzylinder",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "ProductGalery",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "NGF",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Knayf_Options",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Halbzylinder_Options",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Aussenzylinder_Rundzylinder",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Aussen_Rund_all",
                type: "varbinary(max)",
                nullable: true);
  
        }
    }
}
