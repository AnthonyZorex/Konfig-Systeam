namespace schliessanlagen_konfigurator.Models.OrdersOpen
{
    public class Orders
    {
        public int Id { get; set; }
        public string userKey { get; set; }
        public string? DorName { get; set; }
        public string? ProjektName { get; set; }
        public int ZylinderId { get; set; }
        public float? aussen { get; set; }
        public float? innen { get; set; }
        public int? Count { get; set; }
        public string? Options { get; set; }
        public DateTime? Created { get; set; }
        public ICollection<isOpen_Order> isOpen_Order { get; set; }
        public Orders()
        {
            isOpen_Order = new List<isOpen_Order>();
        }

    }
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
    public class isOpen_value
    {
        public int Id { get; set; }
        public int? isOpen_OrderId { get; set; }
        public isOpen_Order isOpen_Order { get; set; }
        public int CountKey { get; set; }
        public string? NameKey { get; set; }
        public string? ForNameKey { get; set; }
        public ICollection<KeyValue> KeyValue { get; set; }
        public isOpen_value()
        {
            KeyValue = new List<KeyValue>();
        }
    }
    public class KeyValue
    {
        public int Id { get; set; }
        public int? OpenKeyId { get; set; }
        public isOpen_value OpenKey { get; set; }
        public bool isOpen { get; set; }
    }
}
