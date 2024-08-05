using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models.System
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
    public class SystemOptionInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? OptionsId { get; set; }
        public SystemOptionen Options { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }
        public ICollection<SystemScheker> SystemScheker { get; set; }
        public ICollection<SystemOptionValue> SystemOptionValue { get; set; }
        public SystemOptionInfo()
        {
            SystemScheker = new List<SystemScheker>();
            SystemOptionValue = new List<SystemOptionValue>();
        }
    }
    public class SystemOptionValue
    {
        public int Id { get; set; }
        public int? SysteamPriceKeyId { get; set; }
        public SystemOptionInfo SysteamPriceKey { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
    }
    public class SystemScheker
    {
        public int Id { get; set; }
        public int? chekerId { get; set; }
        public SystemOptionInfo cheker { get; set; }
        public bool doppel { get; set; }
        public bool Knayf { get; set; }
        public bool Halb { get; set; }
        public bool Hebel { get; set; }
        public bool Vorhang { get; set; }
        public bool Aussen { get; set; }
    }
}
