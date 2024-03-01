using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;

namespace schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder
{
    public class Profil_Knaufzylinder_Options
    {
        public int Id { get; set; }
        public int? Profil_KnaufzylinderId { get; set; }
        public Profil_Knaufzylinder Profil_Knaufzylinder { get; set; }
        public ICollection<Knayf_Options> options { get; set; }

        public Profil_Knaufzylinder_Options()
        {
            options = new List<Knayf_Options>();
        }
    }
}
