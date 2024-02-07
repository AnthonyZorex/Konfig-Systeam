using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class sf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Aussenzylinder_Rundzylinder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schliessanlagenId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussenzylinder_Rundzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussenzylinder_Rundzylinder_Schliessanlagen_schliessanlagenId",
                        column: x => x.schliessanlagenId,
                        principalTable: "Schliessanlagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hebelzylinder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schliessanlagenId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    max = table.Column<double>(type: "float", nullable: false),
                    min = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hebelzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hebelzylinder_Schliessanlagen_schliessanlagenId",
                        column: x => x.schliessanlagenId,
                        principalTable: "Schliessanlagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profil_Doppelzylinder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schliessanlagenId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extern = table.Column<double>(type: "float", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Intern = table.Column<double>(type: "float", nullable: false),
                    max = table.Column<double>(type: "float", nullable: false),
                    min = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil_Doppelzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Doppelzylinder_Schliessanlagen_schliessanlagenId",
                        column: x => x.schliessanlagenId,
                        principalTable: "Schliessanlagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profil_Halbzylinder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schliessanlagenId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Außen = table.Column<double>(type: "float", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    max = table.Column<double>(type: "float", nullable: false),
                    min = table.Column<double>(type: "float", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Profil_Knaufzylinder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schliessanlagenId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extern = table.Column<double>(type: "float", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Intern = table.Column<double>(type: "float", nullable: false),
                    max = table.Column<double>(type: "float", nullable: false),
                    min = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil_Knaufzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Knaufzylinder_Schliessanlagen_schliessanlagenId",
                        column: x => x.schliessanlagenId,
                        principalTable: "Schliessanlagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vorhangschloss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schliessanlagenId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vorhangschloss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vorhangschloss_Schliessanlagen_schliessanlagenId",
                        column: x => x.schliessanlagenId,
                        principalTable: "Schliessanlagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NGFunktion = table.Column<string>(name: "NG_Funktion", type: "nvarchar(max)", nullable: true),
                    Freilauf = table.Column<bool>(type: "bit", nullable: true),
                    Zylinderfärbung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bohrschutz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Witterungsschutz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schließbart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bügelhöhe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zylinderknauf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schließweg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schließhebel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProfilDoppelzylinder = table.Column<int>(name: "IdProfil_Doppelzylinder", type: "int", nullable: true),
                    IdVorhangschloss = table.Column<int>(type: "int", nullable: true),
                    IdProfilKnaufzylinder = table.Column<int>(name: "IdProfil_Knaufzylinder", type: "int", nullable: true),
                    IdProfilHalbzylinder = table.Column<int>(name: "IdProfil_Halbzylinder", type: "int", nullable: true),
                    IdHebelzylinder = table.Column<int>(type: "int", nullable: true),
                    IdAussenzylinderRundzylinder = table.Column<int>(name: "IdAussenzylinder_Rundzylinder", type: "int", nullable: true),
                    AussenzylinderRundzylinderId = table.Column<int>(name: "Aussenzylinder_RundzylinderId", type: "int", nullable: true),
                    HebelzylinderId = table.Column<int>(type: "int", nullable: true),
                    ProfilDoppelzylinderId = table.Column<int>(name: "Profil_DoppelzylinderId", type: "int", nullable: true),
                    ProfilHalbzylinderId = table.Column<int>(name: "Profil_HalbzylinderId", type: "int", nullable: true),
                    ProfilKnaufzylinderId = table.Column<int>(name: "Profil_KnaufzylinderId", type: "int", nullable: true),
                    VorhangschlossId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Aussenzylinder_Rundzylinder_Aussenzylinder_RundzylinderId",
                        column: x => x.AussenzylinderRundzylinderId,
                        principalTable: "Aussenzylinder_Rundzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Hebelzylinder_HebelzylinderId",
                        column: x => x.HebelzylinderId,
                        principalTable: "Hebelzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Profil_Doppelzylinder_Profil_DoppelzylinderId",
                        column: x => x.ProfilDoppelzylinderId,
                        principalTable: "Profil_Doppelzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Profil_Halbzylinder_Profil_HalbzylinderId",
                        column: x => x.ProfilHalbzylinderId,
                        principalTable: "Profil_Halbzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Profil_Knaufzylinder_Profil_KnaufzylinderId",
                        column: x => x.ProfilKnaufzylinderId,
                        principalTable: "Profil_Knaufzylinder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Vorhangschloss_VorhangschlossId",
                        column: x => x.VorhangschlossId,
                        principalTable: "Vorhangschloss",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aussenzylinder_Rundzylinder_schliessanlagenId",
                table: "Aussenzylinder_Rundzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Hebelzylinder_schliessanlagenId",
                table: "Hebelzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Aussenzylinder_RundzylinderId",
                table: "Options",
                column: "Aussenzylinder_RundzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_HebelzylinderId",
                table: "Options",
                column: "HebelzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Profil_DoppelzylinderId",
                table: "Options",
                column: "Profil_DoppelzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Profil_HalbzylinderId",
                table: "Options",
                column: "Profil_HalbzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Profil_KnaufzylinderId",
                table: "Options",
                column: "Profil_KnaufzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_VorhangschlossId",
                table: "Options",
                column: "VorhangschlossId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Doppelzylinder_schliessanlagenId",
                table: "Profil_Doppelzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Halbzylinder_schliessanlagenId",
                table: "Profil_Halbzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Knaufzylinder_schliessanlagenId",
                table: "Profil_Knaufzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorhangschloss_schliessanlagenId",
                table: "Vorhangschloss",
                column: "schliessanlagenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

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
