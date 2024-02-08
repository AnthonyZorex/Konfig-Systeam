namespace schliessanlagen_konfigurator.Models
{
    public class SysteamList
    {
        public IEnumerable<Profil_Doppelzylinder> Doppelzylinder { get; set; } = new List<Profil_Doppelzylinder>();
        public IEnumerable<Profil_Halbzylinder> Halbzylinder { get; set; } = new List<Profil_Halbzylinder>();
        public IEnumerable<Profil_Knaufzylinder> Knaufzylinder { get; set; } = new List<Profil_Knaufzylinder>();
        public IEnumerable<Hebelzylinder> Hebelzylinder { get; set; } = new List<Hebelzylinder>();
        public IEnumerable<Vorhangschloss> Vorhangschloss { get; set; } = new List<Vorhangschloss>();
        public IEnumerable<Aussenzylinder_Rundzylinder> Aussenzylinder_Rundzylinder { get; set; } = new List<Aussenzylinder_Rundzylinder>();
        public IEnumerable<Options> options { get; set; } = new List<Options>();
        public string? Name { get; set; }
    }
}
