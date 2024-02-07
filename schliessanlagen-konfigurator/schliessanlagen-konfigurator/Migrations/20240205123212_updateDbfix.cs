using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class updateDbfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Schließhebel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Aussenzylinder_Rundzylinder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schliessanlagenId = table.Column<int>(type: "int", nullable: false),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    max = table.Column<float>(type: "real", nullable: false),
                    min = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussenzylinder_Rundzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussenzylinder_Rundzylinder_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Options",
                        principalColumn: "Id");
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
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    max = table.Column<float>(type: "real", nullable: false),
                    min = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hebelzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hebelzylinder_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Options",
                        principalColumn: "Id");
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
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extern = table.Column<float>(type: "real", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Intern = table.Column<float>(type: "real", nullable: false),
                    max = table.Column<float>(type: "real", nullable: false),
                    min = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil_Doppelzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Doppelzylinder_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Options",
                        principalColumn: "Id");
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
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_Profil_Halbzylinder_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Options",
                        principalColumn: "Id");
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
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extern = table.Column<float>(type: "real", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Intern = table.Column<float>(type: "real", nullable: false),
                    max = table.Column<float>(type: "real", nullable: false),
                    min = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil_Knaufzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Knaufzylinder_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Options",
                        principalColumn: "Id");
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
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    max = table.Column<float>(type: "real", nullable: false),
                    min = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vorhangschloss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vorhangschloss_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Options",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vorhangschloss_Schliessanlagen_schliessanlagenId",
                        column: x => x.schliessanlagenId,
                        principalTable: "Schliessanlagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aussenzylinder_Rundzylinder_OptionsId",
                table: "Aussenzylinder_Rundzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussenzylinder_Rundzylinder_schliessanlagenId",
                table: "Aussenzylinder_Rundzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Hebelzylinder_OptionsId",
                table: "Hebelzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Hebelzylinder_schliessanlagenId",
                table: "Hebelzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Doppelzylinder_OptionsId",
                table: "Profil_Doppelzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Doppelzylinder_schliessanlagenId",
                table: "Profil_Doppelzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Halbzylinder_OptionsId",
                table: "Profil_Halbzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Halbzylinder_schliessanlagenId",
                table: "Profil_Halbzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Knaufzylinder_OptionsId",
                table: "Profil_Knaufzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Knaufzylinder_schliessanlagenId",
                table: "Profil_Knaufzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorhangschloss_OptionsId",
                table: "Vorhangschloss",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorhangschloss_schliessanlagenId",
                table: "Vorhangschloss",
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
                name: "Options");

            migrationBuilder.DropTable(
                name: "Schliessanlagen");
        }
    }
}
