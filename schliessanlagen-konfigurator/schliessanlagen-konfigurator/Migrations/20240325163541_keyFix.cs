using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class keyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyValue_isOpen_value_isOpen_valueId",
                table: "KeyValue");

            migrationBuilder.DropColumn(
                name: "Open_OrderId",
                table: "KeyValue");

            migrationBuilder.AlterColumn<int>(
                name: "isOpen_valueId",
                table: "KeyValue",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyValue_isOpen_value_isOpen_valueId",
                table: "KeyValue",
                column: "isOpen_valueId",
                principalTable: "isOpen_value",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyValue_isOpen_value_isOpen_valueId",
                table: "KeyValue");

            migrationBuilder.AlterColumn<int>(
                name: "isOpen_valueId",
                table: "KeyValue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Open_OrderId",
                table: "KeyValue",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyValue_isOpen_value_isOpen_valueId",
                table: "KeyValue",
                column: "isOpen_valueId",
                principalTable: "isOpen_value",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
