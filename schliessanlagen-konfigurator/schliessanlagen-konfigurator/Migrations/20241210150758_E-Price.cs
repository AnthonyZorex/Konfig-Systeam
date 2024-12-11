using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class EPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "E_Price",
                table: "ProductSysteam",
                type: "real",
                nullable: true);   
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.DropColumn(
                name: "E_Price",
                table: "ProductSysteam");

        }
    }
}
