using schliessanlagen_konfigurator.Models.OrdersOpen;
using System.ComponentModel.DataAnnotations;
namespace schliessanlagen_konfigurator.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kein NachName angegeben")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kein VorName angegeben")]
        public string Sername { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Kein VorName angegeben")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Kein Login angegeben")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Kein Password angegeben")]
        public string Password { get; set; }
        public string Status { get; set; }
        public string? Adress { get; set; }
        public ICollection<UserOrdersShop> UserOrdersShop { get; set; }
        public User()
        {
            UserOrdersShop= new List<UserOrdersShop>(); 
        }
    }
}
