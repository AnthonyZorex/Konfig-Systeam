using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;

namespace schliessanlagen_konfigurator.Models.Users
{
    public class UserOrdersShop
    {
        public int Id { get; set; }
        public float OrderSum {  get; set; }
        public string ProductName { get; set; }
        public string userkey { get; set; }
        public int? UserId { get; set; }
        public int? Count { get; set; }
        public User User { get; set; }
    }
}
