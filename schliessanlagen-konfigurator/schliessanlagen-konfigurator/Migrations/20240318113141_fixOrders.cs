using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class fixOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "CountKey",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NameKey",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CountKey",
                table: "isOpen_value",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameKey",
                table: "isOpen_value",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountKey",
                table: "isOpen_value");

            migrationBuilder.DropColumn(
                name: "NameKey",
                table: "isOpen_value");

            migrationBuilder.AddColumn<int>(
                name: "CountKey",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameKey",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            
        }
    }
}
