using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class SwitchZylinder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Aussen_Knauf_klein",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aussen = table.Column<float>(type: "real", nullable: false),
                    costSizeAussen = table.Column<float>(type: "real", nullable: false),
                    Aussen_Innen_KnaufId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aussen_Knauf_klein", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aussen_Knauf_klein_Aussen_Innen_Knauf_Aussen_Innen_KnaufId",
                        column: x => x.Aussen_Innen_KnaufId,
                        principalTable: "Aussen_Innen_Knauf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doppel_Aussen_klein",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aussen = table.Column<float>(type: "real", nullable: false),
                    costSizeAussen = table.Column<float>(type: "real", nullable: false),
                    Aussen_InnenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doppel_Aussen_klein", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doppel_Aussen_klein_Aussen_Innen_Aussen_InnenId",
                        column: x => x.Aussen_InnenId,
                        principalTable: "Aussen_Innen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

         
            migrationBuilder.CreateIndex(
                name: "IX_Aussen_Knauf_klein_Aussen_Innen_KnaufId",
                table: "Aussen_Knauf_klein",
                column: "Aussen_Innen_KnaufId");

            migrationBuilder.CreateIndex(
                name: "IX_Doppel_Aussen_klein_Aussen_InnenId",
                table: "Doppel_Aussen_klein",
                column: "Aussen_InnenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aussen_Knauf_klein");

            migrationBuilder.DropTable(
                name: "Doppel_Aussen_klein");

        }
    }
}
