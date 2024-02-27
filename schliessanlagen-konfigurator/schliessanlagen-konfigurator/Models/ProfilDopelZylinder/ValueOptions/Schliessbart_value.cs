namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions
{
    public class Schliessbart_value
    {
        public int Id { get; set; }
        public int? SchliessbartId { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
        public Schliessbart Schliessbart { get; set; }
    }
}
