using schliessanlagen_konfigurator.Models.Vorhan;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models
{
    public class Vorhangschloss
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string? companyName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public float Price { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Vorhan_Options> Vorhan_Options { get; set; }
        public ICollection<Size> Size { get; set; }

        public Vorhangschloss()
        {
            Vorhan_Options = new List<Vorhan_Options>();
            Size = new List<Size>();
        }
    }
}
