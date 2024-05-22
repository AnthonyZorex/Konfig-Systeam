using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Profil_Doppelzylinder
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string? companyName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public float Price { get; set; }
        public bool? isGround { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Profil_Doppelzylinder_Options> Profil_Doppelzylinder_Options { get; set; }
        public ICollection<Aussen_Innen> Aussen_Innen { get; set; }
        public Profil_Doppelzylinder()
        {
            Profil_Doppelzylinder_Options = new List<Profil_Doppelzylinder_Options>();
            Aussen_Innen = new List<Aussen_Innen>();
        }
    }
}
