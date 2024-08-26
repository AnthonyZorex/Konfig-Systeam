using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Schop_models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder
{
    public class Profil_Knaufzylinder: Zylinder_Type
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
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Profil_Knaufzylinder_Options> Profil_Knaufzylinder_Options { get; set; }
        public ICollection<Aussen_Innen_Knauf> Aussen_Innen_Knauf { get; set; }
        public ICollection<ProductGalery> ProductGalery { get; set; }
        public Profil_Knaufzylinder()
        {
            Profil_Knaufzylinder_Options = new List<Profil_Knaufzylinder_Options>();
            Aussen_Innen_Knauf = new List<Aussen_Innen_Knauf>();
            ProductGalery = new List<ProductGalery>();
        }
    }
    public class Profil_Knaufzylinder_Options
    {
        public int Id { get; set; }
        public int? Profil_KnaufzylinderId { get; set; }
        public Profil_Knaufzylinder Profil_Knaufzylinder { get; set; }
        public ICollection<Knayf_Options> options { get; set; }

        public Profil_Knaufzylinder_Options()
        {
            options = new List<Knayf_Options>();
        }
    }
    public class Knayf_Options
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public Profil_Knaufzylinder_Options Options { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }
        public ICollection<Knayf_Options_value> Knayf_Options_value { get; set; }
        public Knayf_Options()
        {
            Knayf_Options_value = new List<Knayf_Options_value>();
        }
    }
    public class Knayf_Options_value
    {
        public int Id { get; set; }
        public int? Knayf_OptionsId { get; set; }
        public Knayf_Options Knayf_Options { get; set; }
        public string? Value { get; set; }
        public float? Cost { get; set; }
    }
}

