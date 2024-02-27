using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;

namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Schliessbart
    {
        public int Id { get; set; }
        public int? dopelOptionsId { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }
        public Profil_Doppelzylinder_Options Options { get; set; }
        public ICollection<Schliessbart_value> Schliessbart_value { get; set; }
        public Schliessbart()
        {
            Schliessbart_value = new List<Schliessbart_value>();
        }
    }
}
