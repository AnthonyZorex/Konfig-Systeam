namespace schliessanlagen_konfigurator.Models.OrdersOpen
{
    public class KeyValue
    {
        public int Id { get; set; }
        public int? OpenKeyId { get; set; }
        public isOpen_value OpenKey { get; set; }
        public bool isOpen { get; set; }
    }
}
