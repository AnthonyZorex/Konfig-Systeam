using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class GalerryFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SysteamPriceKeyId",
                table: "ProductGalery",
                type: "int",
                nullable: true);
   
            migrationBuilder.CreateIndex(
                name: "IX_ProductGalery_SysteamPriceKeyId",
                table: "ProductGalery",
                column: "SysteamPriceKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGalery_SysteamPriceKey_SysteamPriceKeyId",
                table: "ProductGalery",
                column: "SysteamPriceKeyId",
                principalTable: "SysteamPriceKey",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGalery_SysteamPriceKey_SysteamPriceKeyId",
                table: "ProductGalery");

            migrationBuilder.DropIndex(
                name: "IX_ProductGalery_SysteamPriceKeyId",
                table: "ProductGalery");
           
            migrationBuilder.DropColumn(
                name: "SysteamPriceKeyId",
                table: "ProductGalery");

        }
    }
}
