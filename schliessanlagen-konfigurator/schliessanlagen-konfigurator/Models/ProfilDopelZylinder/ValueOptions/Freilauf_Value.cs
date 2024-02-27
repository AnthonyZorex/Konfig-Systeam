namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions
{
    public class Freilauf_Value
    {
        public int Id { get; set; }
        public int? FreilaufId { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
        public Freilauf Freilauf { get; set; }
    }
}
