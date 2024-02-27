namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions
{
    public class Bohrschutz_Value
    {
        public int Id { get; set; }
        public int? BohrschutzId { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
        public Bohrschutz Bohrschutz { get; set; }
    }
}
