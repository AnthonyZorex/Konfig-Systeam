namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions
{
    public class Witterungsschutz_Value
    {
        public int Id { get; set; }
        public int? WitterungsschutzId { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
        public Witterungsschutz Witterungsschutz { get; set; }
    }
}
