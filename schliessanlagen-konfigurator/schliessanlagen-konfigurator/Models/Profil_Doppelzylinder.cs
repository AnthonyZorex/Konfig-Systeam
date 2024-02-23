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
        public string? companyName { get; set; }
        public string ImageName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public float aussen { get; set; }
        public string? Artikelnummer { get; set; }
        public float? Cost { get; set; }
        public float Intern { get; set; } 
        public float maxSizeAussen { get; set; }
        public float minSizeAussen { get; set; }
        public float maxSizeIntern { get; set; }
        public float minSizeIntern { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        
    }
}
