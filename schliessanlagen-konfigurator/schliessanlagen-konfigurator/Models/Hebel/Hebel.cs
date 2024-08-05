using schliessanlagen_konfigurator.Models.System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace schliessanlagen_konfigurator.Models.Hebel
{
    public class Hebel
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string? companyName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public float Price { get; set; }
        public float? Gramm { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Hebelzylinder_Options> Hebelzylinder_Options { get; set; }
        public ICollection<ProductGalery> ProductGalery { get; set; }
        public Hebel()
        {
            Hebelzylinder_Options = new List<Hebelzylinder_Options>();
            ProductGalery = new List<ProductGalery>();
        }

    }
    public class Hebelzylinder_Options
    {
        public int Id { get; set; }
        public int? HebelzylinderId { get; set; }
        public Hebel Hebelzylinder { get; set; }
        public ICollection<Options> Options { get; set; }

        public Hebelzylinder_Options()
        {
            Options = new List<Options>();
        }
    }
    public class Options
    {
        public int Id { get; set; }
        public int? OptionId { get; set; }
        public Hebelzylinder_Options Option { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }

        public ICollection<Options_value> Options_value { get; set; }
        public Options()
        {
            Options_value = new List<Options_value>();
        }
    }
    public class Options_value
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public Options Options { get; set; }
        public string? Value { get; set; }
        public float? Cost { get; set; }
    }
}
