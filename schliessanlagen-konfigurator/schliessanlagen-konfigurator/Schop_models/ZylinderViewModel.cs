namespace schliessanlagen_konfigurator.Schop_models
{
    public class ZylinderViewModel
    {
        public int TotalPages { get; set; }
        public string? Sort_string { get; set; }
        public List<dynamic> ZylinderItems { get; set; } 
        public float? PriceVon { get; set; }
        public float? PriceBis { get; set; }
        public string? Herschteller { get; set; }
        public int page { get; set; }
        public string Typ { get; set; }
    }
}
