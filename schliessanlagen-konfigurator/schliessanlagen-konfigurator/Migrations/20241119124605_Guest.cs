using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Guest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {  
            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "UserOrdersShop",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorhname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bestelung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    news = table.Column<bool>(type: "bit", nullable: false),
                    Liefer_Land = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liefer_Straße = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liefer_Postleitzahl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liefer_Stadt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orderId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.Id);
                });
         
            migrationBuilder.CreateIndex(
                name: "IX_UserOrdersShop_GuestId",
                table: "UserOrdersShop",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrdersShop_Guest_GuestId",
                table: "UserOrdersShop",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrdersShop_Guest_GuestId",
                table: "UserOrdersShop");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropIndex(
                name: "IX_UserOrdersShop_GuestId",
                table: "UserOrdersShop");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "UserOrdersShop");

        }
    }
}
