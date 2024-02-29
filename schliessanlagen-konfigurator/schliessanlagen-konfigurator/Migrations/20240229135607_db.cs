using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
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
                    userKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZylinderId = table.Column<int>(type: "int", nullable: false),
                    aussen = table.Column<float>(type: "real", nullable: true),
                    innen = table.Column<float>(type: "real", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CountKey = table.Column<int>(type: "int", nullable: false),
                    NameKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOppen = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen2 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen3 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen4 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen5 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen6 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen7 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen8 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen9 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen10 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen11 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen12 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen13 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen14 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen15 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen16 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen17 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen18 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen19 = table.Column<bool>(type: "bit", nullable: false),
                    IsOppen20 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

          
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
                    Count = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
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
                    Count = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
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
                    companyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Count = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Count = table.Column<int>(type: "int", nullable: true),
                    Сost = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
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
                name: "Aussen_Innen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilDoppelzylinderId = table.Column<int>(name: "Profil_DoppelzylinderId", type: "int", nullable: false),
                    aussen = table.Column<float>(type: "real", nullable: false),
                    Intern = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussen_Innen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussen_Innen_Profil_Doppelzylinder_Profil_DoppelzylinderId",
                        column: x => x.ProfilDoppelzylinderId,
                        principalTable: "Profil_Doppelzylinder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profil_Doppelzylinder_Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoppelzylinderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil_Doppelzylinder_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Doppelzylinder_Options_Profil_Doppelzylinder_DoppelzylinderId",
                        column: x => x.DoppelzylinderId,
                        principalTable: "Profil_Doppelzylinder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aussen_Innen_Knauf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilDoppelzylinderId = table.Column<int>(name: "Profil_DoppelzylinderId", type: "int", nullable: false),
                    aussen = table.Column<float>(type: "real", nullable: false),
                    Intern = table.Column<float>(type: "real", nullable: false),
                    ProfilKnaufzylinderId = table.Column<int>(name: "Profil_KnaufzylinderId", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussen_Innen_Knauf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussen_Innen_Knauf_Profil_Doppelzylinder_Profil_DoppelzylinderId",
                        column: x => x.ProfilDoppelzylinderId,
                        principalTable: "Profil_Doppelzylinder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aussen_Innen_Knauf_Profil_Knaufzylinder_Profil_KnaufzylinderId",
                        column: x => x.ProfilKnaufzylinderId,
                        principalTable: "Profil_Knaufzylinder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profil_Knaufzylinder_Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilKnaufzylinderId = table.Column<int>(name: "Profil_KnaufzylinderId", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil_Knaufzylinder_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Knaufzylinder_Options_Profil_Knaufzylinder_Profil_KnaufzylinderId",
                        column: x => x.ProfilKnaufzylinderId,
                        principalTable: "Profil_Knaufzylinder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NGF",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NGF_Profil_Doppelzylinder_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Profil_Doppelzylinder_Options",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Knayf_Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knayf_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knayf_Options_Profil_Knaufzylinder_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Profil_Knaufzylinder_Options",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NGF_Value",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NGFId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: true),
                    KnayfOptionsId = table.Column<int>(name: "Knayf_OptionsId", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGF_Value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NGF_Value_Knayf_Options_Knayf_OptionsId",
                        column: x => x.KnayfOptionsId,
                        principalTable: "Knayf_Options",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NGF_Value_NGF_NGFId",
                        column: x => x.NGFId,
                        principalTable: "NGF",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Innen_Profil_DoppelzylinderId",
                table: "Aussen_Innen",
                column: "Profil_DoppelzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Innen_Knauf_Profil_DoppelzylinderId",
                table: "Aussen_Innen_Knauf",
                column: "Profil_DoppelzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Innen_Knauf_Profil_KnaufzylinderId",
                table: "Aussen_Innen_Knauf",
                column: "Profil_KnaufzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussenzylinder_Rundzylinder_schliessanlagenId",
                table: "Aussenzylinder_Rundzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Hebelzylinder_schliessanlagenId",
                table: "Hebelzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Knayf_Options_OptionsId",
                table: "Knayf_Options",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_NGF_OptionsId",
                table: "NGF",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_NGF_Value_Knayf_OptionsId",
                table: "NGF_Value",
                column: "Knayf_OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_NGF_Value_NGFId",
                table: "NGF_Value",
                column: "NGFId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Doppelzylinder_schliessanlagenId",
                table: "Profil_Doppelzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Doppelzylinder_Options_DoppelzylinderId",
                table: "Profil_Doppelzylinder_Options",
                column: "DoppelzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Halbzylinder_schliessanlagenId",
                table: "Profil_Halbzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Knaufzylinder_schliessanlagenId",
                table: "Profil_Knaufzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Knaufzylinder_Options_Profil_KnaufzylinderId",
                table: "Profil_Knaufzylinder_Options",
                column: "Profil_KnaufzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorhangschloss_schliessanlagenId",
                table: "Vorhangschloss",
                column: "schliessanlagenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aussen_Innen");

            migrationBuilder.DropTable(
                name: "Aussen_Innen_Knauf");

            migrationBuilder.DropTable(
                name: "Aussenzylinder_Rundzylinder");

            migrationBuilder.DropTable(
                name: "Hebelzylinder");

            migrationBuilder.DropTable(
                name: "NGF_Value");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Profil_Halbzylinder");

            migrationBuilder.DropTable(
                name: "Vorhangschloss");

            migrationBuilder.DropTable(
                name: "Knayf_Options");

            migrationBuilder.DropTable(
                name: "NGF");

            migrationBuilder.DropTable(
                name: "Profil_Knaufzylinder_Options");

            migrationBuilder.DropTable(
                name: "Profil_Doppelzylinder_Options");

            migrationBuilder.DropTable(
                name: "Profil_Knaufzylinder");

            migrationBuilder.DropTable(
                name: "Profil_Doppelzylinder");

            migrationBuilder.DropTable(
                name: "Schliessanlagen");
        }
    }
}
