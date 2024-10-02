using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Schop_models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace schliessanlagen_konfigurator.Models.Vorhan
{
    public class Vorhangschloss: Zylinder_Type
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
        public byte[]? ImageData { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Vorhan_Options> Vorhan_Options { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Size> Size { get; set; }
        public ICollection<ProductGalery> ProductGalery { get; set; }
        public Vorhangschloss()
        {
            Vorhan_Options = new List<Vorhan_Options>();
            Size = new List<Size>();
            ProductGalery = new List<ProductGalery>();
        }
    }
    public class Vorhan_Options
    {
        public int Id { get; set; }
        public int? VorhangschlossId { get; set; }
        public Vorhangschloss Vorhangschloss { get; set; }
        public ICollection<OptionsVorhan> Options { get; set; }

        public Vorhan_Options()
        {
            Options = new List<OptionsVorhan>();
        }
    }
    public class OptionsVorhan_value
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public OptionsVorhan Options { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }
    }
    public class OptionsVorhan
    {
        public int Id { get; set; }
        public int? OptionId { get; set; }
        public Vorhan_Options Option { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        public byte[]? ImageData { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }

        public ICollection<OptionsVorhan_value> Options_value { get; set; }
        public OptionsVorhan()
        {
            Options_value = new List<OptionsVorhan_value>();
        }
    }
}
