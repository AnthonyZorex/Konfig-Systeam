using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class SysOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "SystemOptionen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOptionen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemOptionen_SysteamPriceKey_SystemId",
                        column: x => x.SystemId,
                        principalTable: "SysteamPriceKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemOptionInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionsId = table.Column<int>(type: "int", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOptionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemOptionInfo_SystemOptionen_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "SystemOptionen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemOptionValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SysteamPriceKeyId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOptionValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemOptionValue_SystemOptionInfo_SysteamPriceKeyId",
                        column: x => x.SysteamPriceKeyId,
                        principalTable: "SystemOptionInfo",
                        principalColumn: "Id");
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_SystemOptionen_SystemId",
                table: "SystemOptionen",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOptionInfo_OptionsId",
                table: "SystemOptionInfo",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOptionValue_SysteamPriceKeyId",
                table: "SystemOptionValue",
                column: "SysteamPriceKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemOptionValue");

            migrationBuilder.DropTable(
                name: "SystemOptionInfo");

            migrationBuilder.DropTable(
                name: "SystemOptionen");

          
        }
    }
}
