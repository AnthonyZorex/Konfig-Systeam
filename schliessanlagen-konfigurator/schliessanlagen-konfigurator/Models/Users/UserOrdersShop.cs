using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;

namespace schliessanlagen_konfigurator.Models.Users
{
    public class UserOrdersShop
    {
        public int Id { get; set; }
        public float OrderSum {  get; set; }
        public string ProductName { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<ProductSysteam> ProductSysteam { get; set; }
        public UserOrdersShop()
        {
            ProductSysteam = new List<ProductSysteam>();
        }

    }
}
