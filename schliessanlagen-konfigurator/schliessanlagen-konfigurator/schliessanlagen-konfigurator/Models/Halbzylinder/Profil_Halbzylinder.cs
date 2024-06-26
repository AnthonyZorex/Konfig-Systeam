using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace schliessanlagen_konfigurator.Models.Halbzylinder
{
    public class Profil_Halbzylinder
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string? companyName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public string? Artikelnummer { get; set; }
        public float Price { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Profil_Halbzylinder_Options> Profil_Halbzylinder_Options { get; set; }
        public ICollection<Aussen_Innen_Halbzylinder> Aussen_Innen_Halbzylinder { get; set; }
        public Profil_Halbzylinder()
        {
            Profil_Halbzylinder_Options = new List<Profil_Halbzylinder_Options>();
            Aussen_Innen_Halbzylinder = new List<Aussen_Innen_Halbzylinder>();
        }

    }
}
