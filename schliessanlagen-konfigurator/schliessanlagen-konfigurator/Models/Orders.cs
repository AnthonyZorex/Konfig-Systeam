using schliessanlagen_konfigurator.Models.OrdersOpen;

namespace schliessanlagen_konfigurator.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string userKey { get; set; }
        public string? DorName { get; set; }
        public int ZylinderId { get; set; }
        public float? aussen { get; set; }
        public float? innen { get; set; }
        public int? Count { get; set; }
        public string? Options { get; set; }
        public DateTime? Created { get; set; }
        public ICollection<isOpen_Order> isOpen_Order { get; set; }
        public Orders()
        {
            isOpen_Order = new List<isOpen_Order>();
        }

    }
}
