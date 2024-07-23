using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;

namespace schliessanlagen_konfigurator.Models
{
    public class SystemOptionValue
    {
        public int Id { get; set; }
        public int? SysteamPriceKeyId { get; set; }
        public SystemOptionInfo SysteamPriceKey { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
    }
}
