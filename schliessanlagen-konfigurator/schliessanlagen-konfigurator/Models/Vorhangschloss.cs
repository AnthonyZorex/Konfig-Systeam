using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models
{
    public class Vorhangschloss
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public string Name { get; set; }
        public string? Artikelnummer { get; set; }
        public int Count { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Options>? Options { get; set; }
        public Vorhangschloss()
        {
            Options = new List<Options>();
        }
    }
}
