using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class KeyImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "KeyImage",
                table: "SysteamPriceKey",
                type: "nvarchar(max)",
                nullable: true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyImage",
                table: "SysteamPriceKey");
         
        }
    }
}
