namespace schliessanlagen_konfigurator.Models.Hebelzylinder
{
    public class Options_value
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public Options Options { get; set; }
        public string? Value { get; set; }
        public float? Cost { get; set; }
    }
}
