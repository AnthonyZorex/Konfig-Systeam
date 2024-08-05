using Org.BouncyCastle.Asn1.Cms;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models.Users
{
    public class UserOrdersShop
    {
        public int Id { get; set; }
        public float OrderSum { get; set; }
        public string ProductName { get; set; }
        public int? KeyCount { get; set; }
        public float? KeyCost { get; set; }
        public string UserOrderKey {  get; set; }
        public DateTime? createData { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime? BezalenDate { get; set; }
        public string? ShippingStatus { get; set; }
        public int? count { get; set; }
        public ICollection<ProductSysteam> ProductSysteam { get; set; }
        public ICollection<Rehnungs> Rehnungs { get; set; }
        public UserOrdersShop()
        {
            ProductSysteam = new List<ProductSysteam>();
            Rehnungs = new List<Rehnungs>();
        }

    }
    public class ProductSysteam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Aussen { get; set; }
        public float? Intern { get; set; }
        public string? Option { get; set; }
        public int? UserOrdersShopId { get; set; }
        public int? Count { get; set; }
        public float? Price { get; set; }
        public UserOrdersShop UserOrdersShop { get; set; }

    }
    public class Rehnungs
    {
        public int Id { get; set; }
        public string RehnungsId { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public List<IFormFile> File { get; set; }
        public int UserOrdersShopId { get; set; }
        public UserOrdersShop UserOrdersShop { get; set; }
    }
}
