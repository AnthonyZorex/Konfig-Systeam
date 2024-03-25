using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class keyFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyValue_isOpen_value_isOpen_valueId",
                table: "KeyValue");

            migrationBuilder.RenameColumn(
                name: "isOpen_valueId",
                table: "KeyValue",
                newName: "OpenKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_KeyValue_isOpen_valueId",
                table: "KeyValue",
                newName: "IX_KeyValue_OpenKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyValue_isOpen_value_OpenKeyId",
                table: "KeyValue",
                column: "OpenKeyId",
                principalTable: "isOpen_value",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyValue_isOpen_value_OpenKeyId",
                table: "KeyValue");

            migrationBuilder.RenameColumn(
                name: "OpenKeyId",
                table: "KeyValue",
                newName: "isOpen_valueId");

            migrationBuilder.RenameIndex(
                name: "IX_KeyValue_OpenKeyId",
                table: "KeyValue",
                newName: "IX_KeyValue_isOpen_valueId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyValue_isOpen_value_isOpen_valueId",
                table: "KeyValue",
                column: "isOpen_valueId",
                principalTable: "isOpen_value",
                principalColumn: "Id");
        }
    }
}
