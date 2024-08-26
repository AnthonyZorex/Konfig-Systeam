using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.Vorhan;
using schliessanlagen_konfigurator.Models.Hebel;
namespace schliessanlagen_konfigurator.Schop_models
{
    public class Zylinder_Type
    {
        public float Price { get; set; }
    }
    public class All_Zylinder_Schop
    {
        public List<Profil_Doppelzylinder> profil_Doppelzylinder { get; set; }
        public List<Profil_Knaufzylinder> profil_Knaufzylinders { get; set; }
        public List<Profil_Halbzylinder> halbzylinders { get; set; }
        public List<Hebel> hebels { get; set; }
        public List<Vorhangschloss> vorhangschlosses { get; set; }
        public List<Aussenzylinder_Rundzylinder> aussenzylinder_Rundzylinders { get; set; }

        public All_Zylinder_Schop()
        {
            profil_Doppelzylinder = new List<Profil_Doppelzylinder>();
            profil_Knaufzylinders = new List<Profil_Knaufzylinder>();
            halbzylinders = new List<Profil_Halbzylinder>();
            hebels = new List<Hebel>();
            vorhangschlosses = new List<Vorhangschloss>();
            aussenzylinder_Rundzylinders = new List<Aussenzylinder_Rundzylinder>();
        }
    }
}
