using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schliessanlagen_konfigurator.Migrations
{
    /// <inheritdoc />
    public partial class OptionsVorhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionsVorhan_Vorhan_Options_OptionId",
                table: "OptionsVorhan");

           

            migrationBuilder.DropColumn(
                name: "OptioId",
                table: "OptionsVorhan");

            migrationBuilder.AlterColumn<int>(
                name: "OptionId",
                table: "OptionsVorhan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");


            migrationBuilder.AddForeignKey(
                name: "FK_OptionsVorhan_Vorhan_Options_OptionId",
                table: "OptionsVorhan",
                column: "OptionId",
                principalTable: "Vorhan_Options",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionsVorhan_Vorhan_Options_OptionId",
                table: "OptionsVorhan");

          

            migrationBuilder.AlterColumn<int>(
                name: "OptionId",
                table: "OptionsVorhan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptioId",
                table: "OptionsVorhan",
                type: "int",
                nullable: true);


            migrationBuilder.AddForeignKey(
                name: "FK_OptionsVorhan_Vorhan_Options_OptionId",
                table: "OptionsVorhan",
                column: "OptionId",
                principalTable: "Vorhan_Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
