namespace schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions
{
    public class Halbzylinder_Options_value
    {
        public int Id { get; set; }
        public int? Halbzylinder_OptionsId { get; set; }
        public Halbzylinder_Options Halbzylinder_Options { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
    }
}
