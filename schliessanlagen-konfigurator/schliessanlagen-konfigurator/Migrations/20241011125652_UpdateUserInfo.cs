using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Land",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Postleitzahl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Stadt",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liefer_Straße",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rechnun_Land",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rechnun_Postleitzahl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rechnun_Stadt",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rechnun_Straße",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Liefer_Land",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Liefer_Postleitzahl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Liefer_Stadt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Liefer_Straße",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rechnun_Land",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rechnun_Postleitzahl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rechnun_Stadt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rechnun_Straße",
                table: "AspNetUsers");

        }
    }
}
