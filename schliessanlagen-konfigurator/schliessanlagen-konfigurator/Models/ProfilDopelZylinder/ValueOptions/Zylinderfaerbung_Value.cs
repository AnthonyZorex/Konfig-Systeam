namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions
{
    public class Zylinderfaerbung_Value
    {
        public int Id { get; set; }
        public int? ZylinderfaerbungId { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
        public Zylinderfaerbung Zylinderfaerbung { get; set; }
    }
}
