using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Hebel;
using schliessanlagen_konfigurator.Models.Vorhan;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
namespace schliessanlagen_konfigurator.Models.System
{
    public class ProductGalery
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public byte[]? ImageData { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public List<IFormFile> Images { get; set; }
        public int? DopelZylinderId { get; set; }
        public Profil_Doppelzylinder DopelZylinder { get; set; }
        public int? SysteamPriceKeyId { get; set; }
        public SysteamPriceKey SysteamPriceKey { get; set; }
        public int? Profil_KnaufzylinderId { get; set; }
        public Profil_Knaufzylinder Profil_Knaufzylinder { get; set; }
        public int? Profil_HalbzylinderId { get; set; }
        public Profil_Halbzylinder Profil_Halbzylinder { get; set; }
        public int? HebelId { get; set; }
        public Hebel.Hebel Hebel { get; set; }
        public int? VorhangschlossId { get; set; }
        public Vorhangschloss Vorhangschloss { get; set; }
        public int? Aussenzylinder_RundzylinderId { get; set; }
        public Aussenzylinder_Rundzylinder Aussenzylinder_Rundzylinder { get; set; }
    }
}
