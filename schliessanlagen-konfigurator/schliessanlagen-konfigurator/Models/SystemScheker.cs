using schliessanlagen_konfigurator.Models.OrdersOpen;

namespace schliessanlagen_konfigurator.Models
{
    public class SystemScheker
    {
        public int Id { get; set; }
        public int? chekerId { get; set; }
        public SystemOptionInfo cheker { get; set; }
        public bool doppel { get; set; }
        public bool Knayf { get; set; }
        public bool Halb { get; set; }
        public bool Hebel { get; set; }
        public bool Vorhang { get; set; }
        public bool Aussen { get; set; }
    }
}
