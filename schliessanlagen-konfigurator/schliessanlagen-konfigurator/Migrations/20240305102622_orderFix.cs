using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class orderFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tur",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Tur",
                table: "isOpen_value",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tur",
                table: "isOpen_value");

            migrationBuilder.AddColumn<string>(
                name: "Tur",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
