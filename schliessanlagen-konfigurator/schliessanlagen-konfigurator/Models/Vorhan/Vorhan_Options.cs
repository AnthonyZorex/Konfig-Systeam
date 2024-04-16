namespace schliessanlagen_konfigurator.Models.Vorhan
{
    public class Vorhan_Options
    {
        public int Id { get; set; }
        public int? VorhangschlossId { get; set; }
        public Vorhangschloss Vorhangschloss { get; set; }
        public ICollection<OptionsVorhan> Options { get; set; }

        public Vorhan_Options()
        {
            Options = new List<OptionsVorhan>();
        }
    }
}
