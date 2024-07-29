using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using System.Collections.ObjectModel;

namespace schliessanlagen_konfigurator.Models
{
    public class SysteamPriceKey
    {
        public int Id { get; set; }
        public string NameSysteam { get; set; }
        public float Price { get; set; }
        public string? DesctiptionsSysteam { get; set; }
        public string? Lieferzeit { get; set; }
        public string? LieferzeitGrosse { get; set; }
        public ICollection<SystemOptionen>? SystemOptionen { get; set; }
        public ICollection<ProductGalery>? ProductGalery { get; set; }
        public SysteamPriceKey()
        {
            SystemOptionen = new List<SystemOptionen>();
            ProductGalery = new List<ProductGalery>();
        }
    }
}
