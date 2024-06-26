using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class OptionsProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "Option",
                table: "ProductSysteam");

            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productSysteamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOptions_ProductSysteam_productSysteamId",
                        column: x => x.productSysteamId,
                        principalTable: "ProductSysteam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptionsValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductOptionsId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductOptionsId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptionsValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOptionsValue_ProductOptions_ProductOptionsId1",
                        column: x => x.ProductOptionsId1,
                        principalTable: "ProductOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_productSysteamId",
                table: "ProductOptions",
                column: "productSysteamId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionsValue_ProductOptionsId1",
                table: "ProductOptionsValue",
                column: "ProductOptionsId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOptionsValue");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.AddColumn<string>(
                name: "Option",
                table: "ProductSysteam",
                type: "nvarchar(max)",
                nullable: true);
         
        }
    }
}
