using schliessanlagen_konfigurator.Models.OrdersOpen;

namespace schliessanlagen_konfigurator.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sername { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public ICollection<UserOrdersShop> UserOrdersShop { get; set; }
        public User()
        {
            UserOrdersShop= new List<UserOrdersShop>(); 
        }
    }
}
