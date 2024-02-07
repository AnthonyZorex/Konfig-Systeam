using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schliessanlagenkonfigurator.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bügelhöhe",
                table: "Vorhangschloss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vorhangschloss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Bohrschutz",
                table: "Profil_Knaufzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Profil_Knaufzylinder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Schließbart",
                table: "Profil_Knaufzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Witterungsschutz",
                table: "Profil_Knaufzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zylinderfärbung",
                table: "Profil_Knaufzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zylinderknauf",
                table: "Profil_Knaufzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bohrschutz",
                table: "Profil_Halbzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Profil_Halbzylinder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Schließbart",
                table: "Profil_Halbzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Witterungsschutz",
                table: "Profil_Halbzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zylinderfärbung",
                table: "Profil_Halbzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bohrschutz",
                table: "Profil_Doppelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Freilauf",
                table: "Profil_Doppelzylinder",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NG_Funktion",
                table: "Profil_Doppelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Profil_Doppelzylinder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Schließbart",
                table: "Profil_Doppelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Witterungsschutz",
                table: "Profil_Doppelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zylinderfärbung",
                table: "Profil_Doppelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Hebelzylinder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Schließhebel",
                table: "Hebelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schließweg",
                table: "Hebelzylinder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Aussenzylinder_Rundzylinder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bügelhöhe",
                table: "Vorhangschloss");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vorhangschloss");

            migrationBuilder.DropColumn(
                name: "Bohrschutz",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Schließbart",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Witterungsschutz",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Zylinderfärbung",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Zylinderknauf",
                table: "Profil_Knaufzylinder");

            migrationBuilder.DropColumn(
                name: "Bohrschutz",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "Schließbart",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "Witterungsschutz",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "Zylinderfärbung",
                table: "Profil_Halbzylinder");

            migrationBuilder.DropColumn(
                name: "Bohrschutz",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Freilauf",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "NG_Funktion",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Schließbart",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Witterungsschutz",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Zylinderfärbung",
                table: "Profil_Doppelzylinder");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Hebelzylinder");

            migrationBuilder.DropColumn(
                name: "Schließhebel",
                table: "Hebelzylinder");

            migrationBuilder.DropColumn(
                name: "Schließweg",
                table: "Hebelzylinder");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Aussenzylinder_Rundzylinder");
        }
    }
}
