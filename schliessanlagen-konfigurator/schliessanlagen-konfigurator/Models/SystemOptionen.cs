namespace schliessanlagen_konfigurator.Models
{
    public class SystemOptionen
    {
        public int Id { get; set; }
        public int SystemId { get; set; }
        public SysteamPriceKey System { get; set; }
        public ICollection<SystemOptionInfo>? SystemOptionInfo { get; set; }
        public SystemOptionen() 
        {
            SystemOptionInfo = new List<SystemOptionInfo>();
        }
    }
}
