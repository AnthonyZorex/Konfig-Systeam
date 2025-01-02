using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class AddCountKeyX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniKeyCount",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UniKeyCount",
                table: "UserOrdersShop",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {       
            migrationBuilder.DropColumn(
                name: "UniKeyCount",
                table: "UserOrdersShop");

            migrationBuilder.AddColumn<int>(
                name: "UniKeyCount",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
