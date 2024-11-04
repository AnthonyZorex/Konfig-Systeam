using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.Vorhan;
using System.Text.Json.Serialization;

namespace schliessanlagen_konfigurator.Models.System
{
    public class Schliessanlagen
    {
        public int Id { get; set; }
        public string nameType { get; set; }
        [JsonIgnore]
        public ICollection<Profil_Doppelzylinder> Profil_Doppelzylinder { get; set; }
        [JsonIgnore]
        public ICollection<Profil_Halbzylinder> Profil_Halbzylinder { get; set; }
        [JsonIgnore]
        public ICollection<Profil_Knaufzylinder> Profil_Knaufzylinder { get; set; }
        [JsonIgnore]
        public ICollection<Hebel.Hebel> Hebelzylinder { get; set; }
        [JsonIgnore]
        public ICollection<Vorhangschloss> Vorhangschloss { get; set; }
        [JsonIgnore]
        public ICollection<Aussenzylinder_Rundzylinder> Aussenzylinder_Rundzylinder { get; set; }
        public Schliessanlagen()
        {
            Profil_Doppelzylinder = new List<Profil_Doppelzylinder>();
            Profil_Halbzylinder = new List<Profil_Halbzylinder>();
            Profil_Knaufzylinder = new List<Profil_Knaufzylinder>();
            Hebelzylinder = new List<Hebel.Hebel>();
            Vorhangschloss = new List<Vorhangschloss>();
            Aussenzylinder_Rundzylinder = new List<Aussenzylinder_Rundzylinder>();
        }
    }
}
