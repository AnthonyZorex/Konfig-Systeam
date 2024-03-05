using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class sdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DorName",
                table: "isOpen_value");

            migrationBuilder.AddColumn<string>(
                name: "DorName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DorName",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "DorName",
                table: "isOpen_value",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
