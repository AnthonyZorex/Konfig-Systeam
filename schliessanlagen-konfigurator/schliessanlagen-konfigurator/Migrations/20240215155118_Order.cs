using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZylinderId = table.Column<int>(type: "int", nullable: false),
                    aussen = table.Column<float>(type: "real", nullable: true),
                    innen = table.Column<float>(type: "real", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CountKey = table.Column<int>(type: "int", nullable: false),
                    NameKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOppen = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen2 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen3 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen4 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen5 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen6 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen7 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen8 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen9 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen10 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen11 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen12 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen13 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen14 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen15 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen16 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen17 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen18 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen19 = table.Column<bool>(type: "bit", nullable: true),
                    IsOppen20 = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Aussenzylinder_Rundzylinder");

            migrationBuilder.DropTable(
                name: "Hebelzylinder");

            migrationBuilder.DropTable(
                name: "Profil_Doppelzylinder");

            migrationBuilder.DropTable(
                name: "Profil_Halbzylinder");

            migrationBuilder.DropTable(
                name: "Profil_Knaufzylinder");

            migrationBuilder.DropTable(
                name: "Vorhangschloss");

            migrationBuilder.DropTable(
                name: "Schliessanlagen");
        }
    }
}
