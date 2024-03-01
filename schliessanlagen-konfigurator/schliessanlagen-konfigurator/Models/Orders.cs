using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;

namespace schliessanlagen_konfigurator.Models
{
    public class Orders
    {
        public int id { get; set; }
        public string userKey { get; set; }
        public string? Tur { get; set; }
        public int ZylinderId { get; set; }
        public float? aussen { get; set; }
        public float? innen { get; set; }
        public int Count { get; set; }
        public int CountKey { get; set; }
        public string? NameKey { get; set; }
        public ICollection<isOpen_Order> isOpen_Order { get; set; }
        public Orders()
        {
            isOpen_Order = new List<isOpen_Order>();
        }

    }
}
