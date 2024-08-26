using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Schop_models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Profil_Doppelzylinder:Zylinder_Type
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string? companyName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public float Price { get; set; }
        public float? Gramm { get; set; }
        public bool? isGround { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Profil_Doppelzylinder_Options> Profil_Doppelzylinder_Options { get; set; }
        public ICollection<Aussen_Innen> Aussen_Innen { get; set; }
        public ICollection<ProductGalery> ProductGalery { get; set; }
        public Profil_Doppelzylinder()
        {
            Profil_Doppelzylinder_Options = new List<Profil_Doppelzylinder_Options>();
            Aussen_Innen = new List<Aussen_Innen>();
            ProductGalery = new List<ProductGalery>();
        }
    }
    public class Profil_Doppelzylinder_Options
    {
        public int Id { get; set; }
        public int? DoppelzylinderId { get; set; }
        public Profil_Doppelzylinder Doppelzylinder { get; set; }
        public ICollection<NGF> NGF { get; set; }
        public Profil_Doppelzylinder_Options()
        {
            NGF = new List<NGF>();
        }
    }
    public class NGF
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public Profil_Doppelzylinder_Options Options { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }
        public ICollection<NGF_Value> NGF_Value { get; set; }
        public NGF()
        {
            NGF_Value = new List<NGF_Value>();
        }
    }
    public class NGF_Value
    {
        public int Id { get; set; }
        public int? NGFId { get; set; }
        public NGF NGF { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }

    }
}
