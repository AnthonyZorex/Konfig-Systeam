using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class priceSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DesctiptionsSysteam",
                table: "SysteamPriceKey",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "costSize",
                table: "Aussen_Innen_Knauf",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "costAussen",
                table: "Aussen_Innen_Halbzylinder",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "costSize",
                table: "Aussen_Innen",
                type: "real",
                nullable: false,
                defaultValue: 0f);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "DesctiptionsSysteam",
                table: "SysteamPriceKey");

            migrationBuilder.DropColumn(
                name: "costSize",
                table: "Aussen_Innen_Knauf");

            migrationBuilder.DropColumn(
                name: "costAussen",
                table: "Aussen_Innen_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "costSize",
                table: "Aussen_Innen");
        }
    }
}
