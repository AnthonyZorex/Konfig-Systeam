using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;

namespace schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder
{
    public class Aussen_Innen_Knauf
    {
        public int Id { get; set; }
        public int Profil_KnaufzylinderId { get; set; }
        public float aussen { get; set; }
        public float Intern { get; set; }
        public float costSizeAussen {  get; set; }
        public float costSizeIntern { get; set; }
        public Profil_Knaufzylinder Profil_Knaufzylinder { get; set; }
        public ICollection<Aussen_Innen_Knauf_klein> Aussen_Innen_Knauf_klein { get; set; }
        public ICollection<Aussen_Knauf_klein> Aussen_Knauf_klein { get; set; }
        public Aussen_Innen_Knauf()
        {
            Aussen_Innen_Knauf_klein = new List<Aussen_Innen_Knauf_klein>();
            Aussen_Knauf_klein = new List<Aussen_Knauf_klein>();
        }
    }
    public class Aussen_Innen_Knauf_klein
    {
        public int Id { get; set; }
        public float Intern { get; set; }
        public float costSizeIntern { get; set; }
        public int Aussen_Innen_KnaufId { get; set; }
        public Aussen_Innen_Knauf Aussen_Innen_Knauf { get; set; }
    }
    public class Aussen_Knauf_klein
    {
        public int Id { get; set; }
        public float aussen { get; set; }
        public float costSizeAussen { get; set; }
        public int Aussen_Innen_KnaufId { get; set; }
        public Aussen_Innen_Knauf Aussen_Innen_Knauf { get; set; }
    }
}
