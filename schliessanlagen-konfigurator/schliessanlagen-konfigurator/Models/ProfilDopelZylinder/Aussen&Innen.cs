

namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Aussen_Innen
    {
        public int Id { get; set; }
        public int Profil_DoppelzylinderId { get; set; }
        public float aussen { get; set; }
        public float Intern { get; set; }
        public Profil_Doppelzylinder Profil_Doppelzylinder { get; set; }
    }
}
