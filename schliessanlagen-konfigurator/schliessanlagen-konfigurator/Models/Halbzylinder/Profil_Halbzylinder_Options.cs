using schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions;

namespace schliessanlagen_konfigurator.Models.Halbzylinder
{
    public class Profil_Halbzylinder_Options
    {
        public int Id { get; set; }
        public int? Profil_HalbzylinderId { get; set; }
        public Profil_Halbzylinder Profil_Halbzylinder { get; set; }
        public ICollection<Halbzylinder_Options> Halbzylinder_Options { get; set; }

        public Profil_Halbzylinder_Options()
        {
            Halbzylinder_Options = new List<Halbzylinder_Options>();
        }
    }
}
