namespace schliessanlagen_konfigurator.Models
{
    public class Options
    {
        public int Id { get; set; }

        public string? NG_Funktion { get; set; }
        public bool? Freilauf { get; set; }
        public string? Zylinderfärbung { get; set; }
        public string? Bohrschutz { get; set; }
        public string? Witterungsschutz { get; set; }
        public string? Schließbart { get; set; }
        public string? Bügelhöhe { get; set; }
        public string? Zylinderknauf { get; set; }
        public string? Schließweg { get; set; }
        public string? Schließhebel { get; set; }
        public string? Modulbauweise { get; set; }
        public string? Zubehör { get; set; }
        public int? IdProfil_Doppelzylinder { get; set; }
        public int? IdVorhangschloss { get; set; }
        public int? IdProfil_Knaufzylinder { get; set; }
        public int? IdProfil_Halbzylinder { get; set; }
        public int? IdHebelzylinder { get; set; }
        public int? IdAussenzylinder_Rundzylinder { get; set; }
    }
}
