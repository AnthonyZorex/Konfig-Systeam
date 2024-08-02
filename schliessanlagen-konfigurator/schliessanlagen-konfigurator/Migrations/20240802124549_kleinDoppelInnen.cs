using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class kleinDoppelInnen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doppel_Innen_klein",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intern = table.Column<float>(type: "real", nullable: false),
                    costSizeIntern = table.Column<float>(type: "real", nullable: false),
                    Aussen_InnenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doppel_Innen_klein", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doppel_Innen_klein_Aussen_Innen_Aussen_InnenId",
                        column: x => x.Aussen_InnenId,
                        principalTable: "Aussen_Innen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doppel_Innen_klein_Aussen_InnenId",
                table: "Doppel_Innen_klein",
                column: "Aussen_InnenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doppel_Innen_klein");

        }
    }
}
