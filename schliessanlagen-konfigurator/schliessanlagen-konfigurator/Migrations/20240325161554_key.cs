using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isOpen",
                table: "isOpen_value");

            migrationBuilder.CreateTable(
                name: "KeyValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenOrderId = table.Column<int>(name: "Open_OrderId", type: "int", nullable: true),
                    isOpenvalueId = table.Column<int>(name: "isOpen_valueId", type: "int", nullable: false),
                    isOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyValue_isOpen_value_isOpen_valueId",
                        column: x => x.isOpenvalueId,
                        principalTable: "isOpen_value",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyValue_isOpen_valueId",
                table: "KeyValue",
                column: "isOpen_valueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyValue");

            migrationBuilder.AddColumn<bool>(
                name: "isOpen",
                table: "isOpen_value",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
