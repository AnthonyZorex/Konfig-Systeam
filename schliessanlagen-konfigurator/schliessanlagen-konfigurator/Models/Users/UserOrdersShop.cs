using Org.BouncyCastle.Asn1.Cms;

namespace schliessanlagen_konfigurator.Models.Users
{
    public class UserOrdersShop
    {
        public int Id { get; set; }
        public float OrderSum { get; set; }
        public string ProductName { get; set; }
        public DateTime? createData { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; }

        public ICollection<ProductSysteam> ProductSysteam { get; set; }
        public UserOrdersShop()
        {
            ProductSysteam = new List<ProductSysteam>();
        }

    }
}
