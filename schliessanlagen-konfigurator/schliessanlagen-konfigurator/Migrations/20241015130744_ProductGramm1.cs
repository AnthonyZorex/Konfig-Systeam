using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class ProductGramm1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Gramm",
                table: "ProductSysteam");

            migrationBuilder.AddColumn<float>(
                name: "Gramm",
                table: "UserOrdersShop",
                type: "real",
                nullable: true);

     
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gramm",
                table: "UserOrdersShop");

            migrationBuilder.AddColumn<float>(
                name: "Gramm",
                table: "ProductSysteam",
                type: "real",
                nullable: true);
        }
    }
}
