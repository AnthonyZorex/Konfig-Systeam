using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class options : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aussenzylinder_Rundzylinder_Options_OptionsId",
                table: "Aussenzylinder_Rundzylinder");

            migrationBuilder.DropForeignKey(
                name: "FK_Hebelzylinder_Options_OptionsId",
                table: "Hebelzylinder");

            migrationBuilder.DropForeignKey(
                name: "FK_Profil_Doppelzylinder_Options_OptionsId",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropForeignKey(
                name: "FK_Profil_Halbzylinder_Options_OptionsId",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropForeignKey(
                name: "FK_Profil_Knaufzylinder_Options_OptionsId",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropForeignKey(
                name: "FK_Vorhangschloss_Options_OptionsId",
                table: "Vorhangschloss");

            migrationBuilder.DropIndex(
                name: "IX_Vorhangschloss_OptionsId",
                table: "Vorhangschloss");

            migrationBuilder.DropIndex(
                name: "IX_Profil_Knaufzylinder_OptionsId",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropIndex(
                name: "IX_Profil_Halbzylinder_OptionsId",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropIndex(
                name: "IX_Profil_Doppelzylinder_OptionsId",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropIndex(
                name: "IX_Hebelzylinder_OptionsId",
                table: "Hebelzylinder");

            migrationBuilder.DropIndex(
                name: "IX_Aussenzylinder_Rundzylinder_OptionsId",
                table: "Aussenzylinder_Rundzylinder");

            migrationBuilder.DropColumn(
                name: "OptionsId",
                table: "Vorhangschloss");

            migrationBuilder.DropColumn(
                name: "OptionsId",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "OptionsId",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "OptionsId",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "OptionsId",
                table: "Hebelzylinder");

            migrationBuilder.DropColumn(
                name: "OptionsId",
                table: "Aussenzylinder_Rundzylinder");

            migrationBuilder.AddColumn<int>(
                name: "Aussenzylinder_RundzylinderId",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HebelzylinderId",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdAussenzylinder_Rundzylinder",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdHebelzylinder",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProfil_Doppelzylinder",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProfil_Halbzylinder",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProfil_Knaufzylinder",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdVorhangschloss",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Profil_DoppelzylinderId",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Profil_HalbzylinderId",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Profil_KnaufzylinderId",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VorhangschlossId",
                table: "Options",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Aussenzylinder_Rundzylinder_Aussenzylinder_RundzylinderId",
                table: "Options",
                column: "Aussenzylinder_RundzylinderId",
                principalTable: "Aussenzylinder_Rundzylinder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Hebelzylinder_HebelzylinderId",
                table: "Options",
                column: "HebelzylinderId",
                principalTable: "Hebelzylinder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Profil_Doppelzylinder_Profil_DoppelzylinderId",
                table: "Options",
                column: "Profil_DoppelzylinderId",
                principalTable: "Profil_Doppelzylinder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Profil_Halbzylinder_Profil_HalbzylinderId",
                table: "Options",
                column: "Profil_HalbzylinderId",
                principalTable: "Profil_Halbzylinder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Profil_Knaufzylinder_Profil_KnaufzylinderId",
                table: "Options",
                column: "Profil_KnaufzylinderId",
                principalTable: "Profil_Knaufzylinder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Vorhangschloss_VorhangschlossId",
                table: "Options",
                column: "VorhangschlossId",
                principalTable: "Vorhangschloss",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Aussenzylinder_Rundzylinder_Aussenzylinder_RundzylinderId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Hebelzylinder_HebelzylinderId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Profil_Doppelzylinder_Profil_DoppelzylinderId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Profil_Halbzylinder_Profil_HalbzylinderId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Profil_Knaufzylinder_Profil_KnaufzylinderId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Vorhangschloss_VorhangschlossId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_Aussenzylinder_RundzylinderId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_HebelzylinderId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_Profil_DoppelzylinderId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_Profil_HalbzylinderId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_Profil_KnaufzylinderId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_VorhangschlossId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Aussenzylinder_RundzylinderId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "HebelzylinderId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "IdAussenzylinder_Rundzylinder",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "IdHebelzylinder",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "IdProfil_Doppelzylinder",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "IdProfil_Halbzylinder",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "IdProfil_Knaufzylinder",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "IdVorhangschloss",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Profil_DoppelzylinderId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Profil_HalbzylinderId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Profil_KnaufzylinderId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "VorhangschlossId",
                table: "Options");

            migrationBuilder.AddColumn<int>(
                name: "OptionsId",
                table: "Vorhangschloss",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionsId",
                table: "Profil_Knaufzylinder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionsId",
                table: "Profil_Halbzylinder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionsId",
                table: "Profil_Doppelzylinder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionsId",
                table: "Hebelzylinder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionsId",
                table: "Aussenzylinder_Rundzylinder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vorhangschloss_OptionsId",
                table: "Vorhangschloss",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Knaufzylinder_OptionsId",
                table: "Profil_Knaufzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Halbzylinder_OptionsId",
                table: "Profil_Halbzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_Doppelzylinder_OptionsId",
                table: "Profil_Doppelzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Hebelzylinder_OptionsId",
                table: "Hebelzylinder",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Aussenzylinder_Rundzylinder_OptionsId",
                table: "Aussenzylinder_Rundzylinder",
                column: "OptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aussenzylinder_Rundzylinder_Options_OptionsId",
                table: "Aussenzylinder_Rundzylinder",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hebelzylinder_Options_OptionsId",
                table: "Hebelzylinder",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profil_Doppelzylinder_Options_OptionsId",
                table: "Profil_Doppelzylinder",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profil_Halbzylinder_Options_OptionsId",
                table: "Profil_Halbzylinder",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profil_Knaufzylinder_Options_OptionsId",
                table: "Profil_Knaufzylinder",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vorhangschloss_Options_OptionsId",
                table: "Vorhangschloss",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id");
        }
    }
}
