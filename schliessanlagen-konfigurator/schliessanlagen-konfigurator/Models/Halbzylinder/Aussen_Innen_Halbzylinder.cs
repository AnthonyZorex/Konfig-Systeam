using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;

namespace schliessanlagen_konfigurator.Models.Halbzylinder
{
    public class Aussen_Innen_Halbzylinder
    {
        public int Id { get; set; }
        public int Profil_HalbzylinderId { get; set; }
        public float aussen { get; set; }
        public Profil_Halbzylinder Profil_Halbzylinder { get; set; }
    }
}
