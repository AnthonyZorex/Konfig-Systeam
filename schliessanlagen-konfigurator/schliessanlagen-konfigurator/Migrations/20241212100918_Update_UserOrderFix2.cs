using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Update_UserOrderFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {          
            migrationBuilder.AddColumn<string>(
                name: "Liefer_Discount",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_E_Mail",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Firma",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Land",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Postleitzahl",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Stadt",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Strasse",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_TelefonNumber",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Ust_Idnr",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Vorname",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_Discount",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_E_Mail",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_Firma",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_Land",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_Postleitzahl",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_Stadt",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_Strasse",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_TelefonNumber",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_Ust_Idnr",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rehnung_Vorname",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: true);     
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "Liefer_Discount",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_E_Mail",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_Firma",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_Land",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_Postleitzahl",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_Stadt",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_Strasse",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_TelefonNumber",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_Ust_Idnr",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Liefer_Vorname",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_Discount",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_E_Mail",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_Firma",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_Land",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_Postleitzahl",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_Stadt",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_Strasse",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_TelefonNumber",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_Ust_Idnr",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "Rehnung_Vorname",
                table: "UserOrdersShop");
        }
    }
}
