using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class bag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

           
           
           
            migrationBuilder.CreateTable(
                name: "Profil_Halbzylinder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schliessanlagenId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Außen = table.Column<float>(type: "real", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    max = table.Column<float>(type: "real", nullable: false),
                    min = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil_Halbzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Halbzylinder_Schliessanlagen_schliessanlagenId",
                        column: x => x.schliessanlagenId,
                        principalTable: "Schliessanlagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Halbzylinder_schliessanlagenId",
                table: "Profil_Halbzylinder",
                column: "schliessanlagenId");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
