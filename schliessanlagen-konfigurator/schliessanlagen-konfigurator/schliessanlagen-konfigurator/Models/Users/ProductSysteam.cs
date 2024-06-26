namespace schliessanlagen_konfigurator.Models.Users
{
    public class ProductSysteam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Aussen { get; set; }
        public float? Intern { get; set; }
        public string? Option { get; set; }
        public int? UserOrdersShopId { get; set; }
        public int? Count { get; set; }
        public UserOrdersShop UserOrdersShop { get; set; }
      
    }
}
