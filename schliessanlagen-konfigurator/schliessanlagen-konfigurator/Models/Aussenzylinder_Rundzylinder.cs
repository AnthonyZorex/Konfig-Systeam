using schliessanlagen_konfigurator.Models.Aussen_Rund;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models
{
    public class Aussenzylinder_Rundzylinder
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
        public ICollection<Aussen_Rund_options> Aussen_Rund_options { get; set; }

        public Aussenzylinder_Rundzylinder()
        {
            Aussen_Rund_options = new List<Aussen_Rund_options>();

        }
    }
}
