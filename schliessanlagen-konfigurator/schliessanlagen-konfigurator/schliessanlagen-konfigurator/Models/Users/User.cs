using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace schliessanlagen_konfigurator.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Address { get; set; } = "";
        public bool? isSend { get; set; } = false;
        public DateTime CreatedAT { get; set; }

        public ICollection<UserOrdersShop> UserOrdersShop { get; set; }
        public User()
        {
            UserOrdersShop = new List<UserOrdersShop>();
        }
    }
}
