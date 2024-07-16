namespace schliessanlagen_konfigurator.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Order { get; set; }
        public DateTime BezalenDate { get; set; }
        public string Status { get; set; }
        public string ShippingStatus {  get; set; }
        public string Total {  get; set; }
    }
}
