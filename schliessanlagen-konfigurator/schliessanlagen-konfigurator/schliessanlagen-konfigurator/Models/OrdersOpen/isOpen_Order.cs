namespace schliessanlagen_konfigurator.Models.OrdersOpen
{
    public class isOpen_Order
    {
        public int Id { get; set; }
        public int? OrdersId { get; set; }
        public Orders Orders { get; set; }
        public ICollection<isOpen_value> isOpen_value { get; set; }

        public isOpen_Order()
        {
            isOpen_value = new List<isOpen_value>();
        }
    }
}
