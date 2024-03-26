using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artikelnummer",
                table: "Vorhangschloss");

            migrationBuilder.DropColumn(
                name: "Artikelnummer",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Artikelnummer",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Artikelnummer",
                table: "Hebelzylinder");

            migrationBuilder.DropColumn(
                name: "Artikelnummer",
                table: "Aussenzylinder_Rundzylinder");

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Artikelnummer",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrdersUser",
                columns: table => new
                {
                    Ordersid = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersUser", x => new { x.Ordersid, x.userId });
                    table.ForeignKey(
                        name: "FK_OrdersUser_Orders_Ordersid",
                        column: x => x.Ordersid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersUser_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersUser_userId",
                table: "OrdersUser",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersUser");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Artikelnummer",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Artikelnummer",
                table: "Vorhangschloss",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Artikelnummer",
                table: "Profil_Knaufzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Artikelnummer",
                table: "Profil_Doppelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Artikelnummer",
                table: "Hebelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Artikelnummer",
                table: "Aussenzylinder_Rundzylinder",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
