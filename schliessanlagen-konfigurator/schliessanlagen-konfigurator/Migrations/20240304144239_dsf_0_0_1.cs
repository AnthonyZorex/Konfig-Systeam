using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class dsf001 : Migration
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
                    NameKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "isOpen_Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isOpen_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_isOpen_Order_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Aussenzylinder_Rundzylinder",
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
                    companyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Artikelnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    table.PrimaryKey("PK_Vorhangschloss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vorhangschloss_Schliessanlagen_schliessanlagenId",
                        column: x => x.schliessanlagenId,
                        principalTable: "Schliessanlagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "isOpen_value",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isOpenOrderId = table.Column<int>(name: "isOpen_OrderId", type: "int", nullable: true),
                    isOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isOpen_value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_isOpen_value_isOpen_Order_isOpen_OrderId",
                        column: x => x.isOpenOrderId,
                        principalTable: "isOpen_Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aussen_Rund_options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AussenzylinderRundzylinderId = table.Column<int>(name: "Aussenzylinder_RundzylinderId", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussen_Rund_options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussen_Rund_options_Aussenzylinder_Rundzylinder_Aussenzylinder_RundzylinderId",
                        column: x => x.AussenzylinderRundzylinderId,
                        principalTable: "Aussenzylinder_Rundzylinder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hebelzylinder_Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HebelzylinderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hebelzylinder_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hebelzylinder_Options_Hebelzylinder_HebelzylinderId",
                        column: x => x.HebelzylinderId,
                        principalTable: "Hebelzylinder",
                        principalColumn: "Id");
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
                name: "Aussen_Innen_Halbzylinder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilHalbzylinderId = table.Column<int>(name: "Profil_HalbzylinderId", type: "int", nullable: false),
                    aussen = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussen_Innen_Halbzylinder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussen_Innen_Halbzylinder_Profil_Halbzylinder_Profil_HalbzylinderId",
                        column: x => x.ProfilHalbzylinderId,
                        principalTable: "Profil_Halbzylinder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profil_Halbzylinder_Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilHalbzylinderId = table.Column<int>(name: "Profil_HalbzylinderId", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil_Halbzylinder_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profil_Halbzylinder_Options_Profil_Halbzylinder_Profil_HalbzylinderId",
                        column: x => x.ProfilHalbzylinderId,
                        principalTable: "Profil_Halbzylinder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aussen_Innen_Knauf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilKnaufzylinderId = table.Column<int>(name: "Profil_KnaufzylinderId", type: "int", nullable: false),
                    aussen = table.Column<float>(type: "real", nullable: false),
                    Intern = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussen_Innen_Knauf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussen_Innen_Knauf_Profil_Knaufzylinder_Profil_KnaufzylinderId",
                        column: x => x.ProfilKnaufzylinderId,
                        principalTable: "Profil_Knaufzylinder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Vorhan_Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VorhangschlossId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vorhan_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vorhan_Options_Vorhangschloss_VorhangschlossId",
                        column: x => x.VorhangschlossId,
                        principalTable: "Vorhangschloss",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aussen_Rund_all",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AussenRundoptionsId = table.Column<int>(name: "Aussen_Rund_optionsId", type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussen_Rund_all", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussen_Rund_all_Aussen_Rund_options_Aussen_Rund_optionsId",
                        column: x => x.AussenRundoptionsId,
                        principalTable: "Aussen_Rund_options",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Hebelzylinder_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Hebelzylinder_Options",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NGF",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Halbzylinder_Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halbzylinder_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halbzylinder_Options_Profil_Halbzylinder_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Profil_Halbzylinder_Options",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Knayf_Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "OptionsVorhan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptioId = table.Column<int>(type: "int", nullable: true),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsVorhan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionsVorhan_Vorhan_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Vorhan_Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aussen_Rouns_all_value",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AussenRundallId = table.Column<int>(name: "Aussen_Rund_allId", type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussen_Rouns_all_value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussen_Rouns_all_value_Aussen_Rund_all_Aussen_Rund_allId",
                        column: x => x.AussenRundallId,
                        principalTable: "Aussen_Rund_all",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Options_value",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options_value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_value_Options_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "Options",
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
                    Cost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGF_Value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NGF_Value_NGF_NGFId",
                        column: x => x.NGFId,
                        principalTable: "NGF",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Halbzylinder_Options_value",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HalbzylinderOptionsId = table.Column<int>(name: "Halbzylinder_OptionsId", type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halbzylinder_Options_value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halbzylinder_Options_value_Halbzylinder_Options_Halbzylinder_OptionsId",
                        column: x => x.HalbzylinderOptionsId,
                        principalTable: "Halbzylinder_Options",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Knayf_Options_value",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KnayfOptionsId = table.Column<int>(name: "Knayf_OptionsId", type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knayf_Options_value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knayf_Options_value_Knayf_Options_Knayf_OptionsId",
                        column: x => x.KnayfOptionsId,
                        principalTable: "Knayf_Options",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OptionsVorhan_value",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsVorhan_value", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionsVorhan_value_OptionsVorhan_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "OptionsVorhan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Innen_Profil_DoppelzylinderId",
                table: "Aussen_Innen",
                column: "Profil_DoppelzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Innen_Halbzylinder_Profil_HalbzylinderId",
                table: "Aussen_Innen_Halbzylinder",
                column: "Profil_HalbzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Innen_Knauf_Profil_KnaufzylinderId",
                table: "Aussen_Innen_Knauf",
                column: "Profil_KnaufzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Rouns_all_value_Aussen_Rund_allId",
                table: "Aussen_Rouns_all_value",
                column: "Aussen_Rund_allId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Rund_all_Aussen_Rund_optionsId",
                table: "Aussen_Rund_all",
                column: "Aussen_Rund_optionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Rund_options_Aussenzylinder_RundzylinderId",
                table: "Aussen_Rund_options",
                column: "Aussenzylinder_RundzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussenzylinder_Rundzylinder_schliessanlagenId",
                table: "Aussenzylinder_Rundzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Halbzylinder_Options_OptionsId",
                table: "Halbzylinder_Options",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Halbzylinder_Options_value_Halbzylinder_OptionsId",
                table: "Halbzylinder_Options_value",
                column: "Halbzylinder_OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Hebelzylinder_schliessanlagenId",
                table: "Hebelzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Hebelzylinder_Options_HebelzylinderId",
                table: "Hebelzylinder_Options",
                column: "HebelzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_isOpen_Order_OrdersId",
                table: "isOpen_Order",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_isOpen_value_isOpen_OrderId",
                table: "isOpen_value",
                column: "isOpen_OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Knayf_Options_OptionsId",
                table: "Knayf_Options",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Knayf_Options_value_Knayf_OptionsId",
                table: "Knayf_Options_value",
                column: "Knayf_OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_NGF_OptionsId",
                table: "NGF",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_NGF_Value_NGFId",
                table: "NGF_Value",
                column: "NGFId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionId",
                table: "Options",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_value_OptionsId",
                table: "Options_value",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionsVorhan_OptionId",
                table: "OptionsVorhan",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionsVorhan_value_OptionsId",
                table: "OptionsVorhan_value",
                column: "OptionsId");

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
                name: "IX_Profil_Halbzylinder_Options_Profil_HalbzylinderId",
                table: "Profil_Halbzylinder_Options",
                column: "Profil_HalbzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Knaufzylinder_schliessanlagenId",
                table: "Profil_Knaufzylinder",
                column: "schliessanlagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Knaufzylinder_Options_Profil_KnaufzylinderId",
                table: "Profil_Knaufzylinder_Options",
                column: "Profil_KnaufzylinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorhan_Options_VorhangschlossId",
                table: "Vorhan_Options",
                column: "VorhangschlossId");

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
                name: "Aussen_Innen_Halbzylinder");

            migrationBuilder.DropTable(
                name: "Aussen_Innen_Knauf");

            migrationBuilder.DropTable(
                name: "Aussen_Rouns_all_value");

            migrationBuilder.DropTable(
                name: "Halbzylinder_Options_value");

            migrationBuilder.DropTable(
                name: "isOpen_value");

            migrationBuilder.DropTable(
                name: "Knayf_Options_value");

            migrationBuilder.DropTable(
                name: "NGF_Value");

            migrationBuilder.DropTable(
                name: "Options_value");

            migrationBuilder.DropTable(
                name: "OptionsVorhan_value");

            migrationBuilder.DropTable(
                name: "Aussen_Rund_all");

            migrationBuilder.DropTable(
                name: "Halbzylinder_Options");

            migrationBuilder.DropTable(
                name: "isOpen_Order");

            migrationBuilder.DropTable(
                name: "Knayf_Options");

            migrationBuilder.DropTable(
                name: "NGF");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "OptionsVorhan");

            migrationBuilder.DropTable(
                name: "Aussen_Rund_options");

            migrationBuilder.DropTable(
                name: "Profil_Halbzylinder_Options");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Profil_Knaufzylinder_Options");

            migrationBuilder.DropTable(
                name: "Profil_Doppelzylinder_Options");

            migrationBuilder.DropTable(
                name: "Hebelzylinder_Options");

            migrationBuilder.DropTable(
                name: "Vorhan_Options");

            migrationBuilder.DropTable(
                name: "Aussenzylinder_Rundzylinder");

            migrationBuilder.DropTable(
                name: "Profil_Halbzylinder");

            migrationBuilder.DropTable(
                name: "Profil_Knaufzylinder");

            migrationBuilder.DropTable(
                name: "Profil_Doppelzylinder");

            migrationBuilder.DropTable(
                name: "Hebelzylinder");

            migrationBuilder.DropTable(
                name: "Vorhangschloss");

            migrationBuilder.DropTable(
                name: "Schliessanlagen");
        }
    }
}
