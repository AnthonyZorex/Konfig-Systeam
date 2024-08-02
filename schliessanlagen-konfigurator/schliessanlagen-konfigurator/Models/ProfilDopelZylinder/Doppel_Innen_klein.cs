namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Doppel_Innen_klein
    {
        public int Id { get; set; }
        public float Intern { get; set; }
        public float costSizeIntern { get; set; }
        public int Aussen_InnenId { get; set; }
        public Aussen_Innen Aussen_Innen { get; set; }
    }
}
