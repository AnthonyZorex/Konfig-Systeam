using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace schliessanlagen_konfigurator.Models
{
    public class Profil_Doppelzylinder
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public double Extern { get; set; } = 30;
        public string? Artikelnummer { get; set; }
        public int Count { get; set; }
        public double Intern { get; set; } = 30;
        public double max { get; set; }
        public double min { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Options>? Options { get; set; }
        public Profil_Doppelzylinder()
        {
            Options = new List<Options>();
        }
    }
}
