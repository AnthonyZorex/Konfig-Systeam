using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class ProductSysteam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "userkey",
                table: "UserOrdersShop");

            migrationBuilder.CreateTable(
                name: "ProductSysteam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aussen = table.Column<float>(type: "real", nullable: false),
                    Intern = table.Column<float>(type: "real", nullable: false),
                    Option = table.Column<float>(type: "real", nullable: false),
                    UserOrdersShopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSysteam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSysteam_UserOrdersShop_UserOrdersShopId",
                        column: x => x.UserOrdersShopId,
                        principalTable: "UserOrdersShop",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSysteam_UserOrdersShopId",
                table: "ProductSysteam",
                column: "UserOrdersShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSysteam");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "UserOrdersShop",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userkey",
                table: "UserOrdersShop",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
