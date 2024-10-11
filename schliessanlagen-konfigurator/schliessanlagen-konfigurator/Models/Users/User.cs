using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace schliessanlagen_konfigurator.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Address { get; set; } = "";
        
        public string? Rechnun_Land { get; set; } = "";
        public string? Rechnun_Straße { get; set; } = "";
        public string? Rechnun_Postleitzahl { get; set; } = "";
        public string? Rechnun_Stadt { get; set; } = "";

        public string? Liefer_Land { get; set; } = "";
        public string? Liefer_Straße { get; set; } = "";
        public string? Liefer_Postleitzahl { get; set; } = "";
        public string? Liefer_Stadt { get; set; } = "";

        public bool? isSend { get; set; } = false;
        public DateTime CreatedAT { get; set; }

        public ICollection<UserOrdersShop> UserOrdersShop { get; set; }
        public User()
        {
            UserOrdersShop = new List<UserOrdersShop>();
        }
    }
    public class UserOrdersShop
    {
        public int Id { get; set; }
        public float OrderSum { get; set; }
        public string ProductName { get; set; }
        public int? KeyCount { get; set; }
        public float? KeyCost { get; set; }
        public string UserOrderKey { get; set; }
        public DateTime? createData { get; set; }
        public string? Lieferzeit { get; set; }
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
