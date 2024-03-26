using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;

namespace schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder
{
    public class Profil_Knaufzylinder
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string? companyName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public float Cost { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Profil_Knaufzylinder_Options> Profil_Knaufzylinder_Options { get; set; }
        public ICollection<Aussen_Innen_Knauf> Aussen_Innen_Knauf { get; set; }
        public Profil_Knaufzylinder()
        {
            Profil_Knaufzylinder_Options = new List<Profil_Knaufzylinder_Options>();
            Aussen_Innen_Knauf = new List<Aussen_Innen_Knauf>();
        }
    }
}
