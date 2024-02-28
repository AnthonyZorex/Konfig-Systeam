using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models
{
    public class Profil_Halbzylinder
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public double Außen { get; set; } = 25;
        public string? Artikelnummer { get; set; }
        public int? Count { get; set; }
        public decimal? Cost { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
       
    }
}
