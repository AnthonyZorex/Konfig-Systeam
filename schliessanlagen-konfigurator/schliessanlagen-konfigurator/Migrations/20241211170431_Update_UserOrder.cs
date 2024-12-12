using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Update_UserOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<float>(
                name: "NettoPrice",
                table: "UserOrdersShop",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Steur",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SteurPrice",
                table: "UserOrdersShop",
                type: "real",
                nullable: true);          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {          
            migrationBuilder.DropColumn(
                name: "NettoPrice",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Steur",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "SteurPrice",
                table: "UserOrdersShop");          
        }
    }
}
