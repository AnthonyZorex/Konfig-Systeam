using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;

namespace schliessanlagen_konfigurator.Models
{
    public class Schliessanlagen
    {
        public int Id { get; set; }
        public string nameType { get; set; }
        public ICollection<Profil_Doppelzylinder> Profil_Doppelzylinder { get; set; }
        public ICollection<Profil_Halbzylinder> Profil_Halbzylinder { get; set; }
        public ICollection<Profil_Knaufzylinder> Profil_Knaufzylinder { get; set; }
        public ICollection<Hebelzylinder> Hebelzylinder { get; set; }
        public ICollection<Vorhangschloss> Vorhangschloss { get; set; }
        public ICollection<Aussenzylinder_Rundzylinder> Aussenzylinder_Rundzylinder { get; set; }
        public Schliessanlagen()
        {
            Profil_Doppelzylinder = new List<Profil_Doppelzylinder>();
            Profil_Halbzylinder = new List<Profil_Halbzylinder>();
            Profil_Knaufzylinder = new List<Profil_Knaufzylinder>();
            Hebelzylinder = new List<Hebelzylinder>();
            Vorhangschloss = new List<Vorhangschloss>();
            Aussenzylinder_Rundzylinder = new List<Aussenzylinder_Rundzylinder>();
        }
    }
}
