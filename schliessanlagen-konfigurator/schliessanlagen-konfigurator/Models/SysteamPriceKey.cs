namespace schliessanlagen_konfigurator.Models
{
    public class SysteamPriceKey
    {
        public int Id { get; set; }
        public string NameSysteam { get; set; }
        public float Price { get; set; }
        public string? DesctiptionsSysteam { get; set; } 
        public string? Lieferzeit { get; set; }
    }
}
