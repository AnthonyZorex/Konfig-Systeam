using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Rehnung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "KeyCost",
                table: "UserOrdersShop",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KeyCount",
                table: "UserOrdersShop",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rehnungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RehnungsId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserOrdersShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rehnungs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rehnungs_UserOrdersShop_UserOrdersShopId",
                        column: x => x.UserOrdersShopId,
                        principalTable: "UserOrdersShop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Rehnungs_UserOrdersShopId",
                table: "Rehnungs",
                column: "UserOrdersShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rehnungs");

            migrationBuilder.DropColumn(
                name: "KeyCost",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "KeyCount",
                table: "UserOrdersShop");
        }
    }
}
