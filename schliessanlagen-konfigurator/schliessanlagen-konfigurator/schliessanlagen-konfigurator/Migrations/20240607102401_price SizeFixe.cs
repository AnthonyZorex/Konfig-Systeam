using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class priceSizeFixe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.RenameColumn(
                name: "costSize",
                table: "Aussen_Innen_Knauf",
                newName: "costSizeIntern");

            migrationBuilder.RenameColumn(
                name: "costSize",
                table: "Aussen_Innen",
                newName: "costSizeIntern");

            migrationBuilder.AddColumn<float>(
                name: "costSizeAussen",
                table: "Aussen_Innen_Knauf",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "costSizeAussen",
                table: "Aussen_Innen",
                type: "real",
                nullable: false,
                defaultValue: 0f);

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.DropColumn(
                name: "costSizeAussen",
                table: "Aussen_Innen_Knauf");

            migrationBuilder.DropColumn(
                name: "costSizeAussen",
                table: "Aussen_Innen");

            migrationBuilder.RenameColumn(
                name: "costSizeIntern",
                table: "Aussen_Innen_Knauf",
                newName: "costSize");

            migrationBuilder.RenameColumn(
                name: "costSizeIntern",
                table: "Aussen_Innen",
                newName: "costSize");

           
        }
    }
}
