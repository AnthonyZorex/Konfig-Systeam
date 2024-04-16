namespace schliessanlagen_konfigurator.Models.Vorhan
{
    public class OptionsVorhan_value
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public OptionsVorhan Options { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
    }
}
