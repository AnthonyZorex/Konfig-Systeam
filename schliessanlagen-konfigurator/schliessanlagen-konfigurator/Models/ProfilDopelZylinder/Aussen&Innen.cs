namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Aussen_Innen
    {
        public int Id { get; set; }
        public int Profil_DoppelzylinderId { get; set; }
        public float aussen { get; set; }
        public float costSizeAussen { get; set; }
        public float costSizeIntern { get; set; }
        public float Intern { get; set; }
        public Profil_Doppelzylinder Profil_Doppelzylinder { get; set; }
        public ICollection<Doppel_Innen_klein> Doppel_Innen_klein { get; set; }
        public Aussen_Innen()
        {
            Doppel_Innen_klein = new List<Doppel_Innen_klein>();
        }
    }
    public class Doppel_Innen_klein
    {
        public int Id { get; set; }
        public float Intern { get; set; }
        public float costSizeIntern { get; set; }
        public int Aussen_InnenId { get; set; }
        public Aussen_Innen Aussen_Innen { get; set; }
    }
}
