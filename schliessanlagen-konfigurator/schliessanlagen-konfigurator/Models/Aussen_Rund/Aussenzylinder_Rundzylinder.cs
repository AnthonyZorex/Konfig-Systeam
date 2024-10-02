using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Schop_models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models.Aussen_Rund
{
    public class Aussenzylinder_Rundzylinder:Zylinder_Type
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string? companyName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public float Price { get; set; }
        public float? Gramm { get; set; }
        public string? Type { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public byte[]? ImageData { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Aussen_Rund_options> Aussen_Rund_options { get; set; }
        public ICollection<ProductGalery> ProductGalery { get; set; }
        public Aussenzylinder_Rundzylinder()
        {
            Aussen_Rund_options = new List<Aussen_Rund_options>();
            ProductGalery = new List<ProductGalery>();
        }
    }
    public class Aussen_Rund_options
    {
        public int Id { get; set; }
        public int? Aussenzylinder_RundzylinderId { get; set; }
        public Aussenzylinder_Rundzylinder Aussenzylinder_Rundzylinder { get; set; }
        public ICollection<Aussen_Rund_all> Aussen_Rund_all { get; set; }

        public Aussen_Rund_options()
        {
            Aussen_Rund_all = new List<Aussen_Rund_all>();
        }
    }
    public class Aussen_Rund_all
    {
        public int Id { get; set; }
        public int? Aussen_Rund_optionsId { get; set; }
        public Aussen_Rund_options Aussen_Rund_options { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        public byte[]? ImageData { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }

        public ICollection<Aussen_Rouns_all_value> Aussen_Rouns_all_value { get; set; }
        public Aussen_Rund_all()
        {
            Aussen_Rouns_all_value = new List<Aussen_Rouns_all_value>();
        }
    }
    public class Aussen_Rouns_all_value
    {
        public int Id { get; set; }
        public int? Aussen_Rund_allId { get; set; }
        public Aussen_Rund_all Aussen_Rund_all { get; set; }
        public string? Value { get; set; }
        public float? Cost { get; set; }
    }
}
