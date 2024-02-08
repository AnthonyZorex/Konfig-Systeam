using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models
{
    public class Hebelzylinder
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public string? Artikelnummer { get; set; }
        public int? Count { get; set; }
        public decimal? Cost { get; set; }

        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Options>? Options { get; set; }
        public Hebelzylinder()
        {
            Options = new List<Options>();
        }
    }
}
