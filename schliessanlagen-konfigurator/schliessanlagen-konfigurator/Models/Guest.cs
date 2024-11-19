using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Models.Users;

namespace schliessanlagen_konfigurator.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Vorhname { get; set; }
        public string Nachname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Bestelung { get; set; }
        public bool news { get; set; } = false;
        public string? Liefer_Land { get; set; } = "";
        public string? Liefer_Straße { get; set; } = "";
        public string? Liefer_Postleitzahl { get; set; } = "";
        public string? Liefer_Stadt { get; set; } = "";
        public string orderId { get; set; }
        public ICollection<UserOrdersShop> UserOrdersShop { get; set; }
        public Guest()
        {       
             UserOrdersShop = new List<UserOrdersShop>();
        }
    }
}
