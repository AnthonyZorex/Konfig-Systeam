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
    }
}
