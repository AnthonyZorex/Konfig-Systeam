using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class Galery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductGalery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DopelZylinderId = table.Column<int>(type: "int", nullable: true),
                    Profil_KnaufzylinderId = table.Column<int>(type: "int", nullable: true),
                    Profil_HalbzylinderId = table.Column<int>(type: "int", nullable: true),
                    HebelId = table.Column<int>(type: "int", nullable: true),
                    VorhangschlossId = table.Column<int>(type: "int", nullable: true),
                    Aussenzylinder_RundzylinderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGalery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGalery_Aussenzylinder_Rundzylinder_Aussenzylinder_RundzylinderId",
                        column: x => x.Aussenzylinder_RundzylinderId,
                        principalTable: "Aussenzylinder_Rundzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductGalery_Hebelzylinder_HebelId",
                        column: x => x.HebelId,
                        principalTable: "Hebelzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductGalery_Profil_Doppelzylinder_DopelZylinderId",
                        column: x => x.DopelZylinderId,
                        principalTable: "Profil_Doppelzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductGalery_Profil_Halbzylinder_Profil_HalbzylinderId",
                        column: x => x.Profil_HalbzylinderId,
                        principalTable: "Profil_Halbzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductGalery_Profil_Knaufzylinder_Profil_KnaufzylinderId",
                        column: x => x.Profil_KnaufzylinderId,
                        principalTable: "Profil_Knaufzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductGalery_Vorhangschloss_VorhangschlossId",
                        column: x => x.VorhangschlossId,
                        principalTable: "Vorhangschloss",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateIndex(
                name: "IX_ProductGalery_Aussenzylinder_RundzylinderId",
                table: "ProductGalery",
                column: "Aussenzylinder_RundzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalery_DopelZylinderId",
                table: "ProductGalery",
                column: "DopelZylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalery_HebelId",
                table: "ProductGalery",
                column: "HebelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalery_Profil_HalbzylinderId",
                table: "ProductGalery",
                column: "Profil_HalbzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalery_Profil_KnaufzylinderId",
                table: "ProductGalery",
                column: "Profil_KnaufzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalery_VorhangschlossId",
                table: "ProductGalery",
                column: "VorhangschlossId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductGalery"); 
        }
    }
}
