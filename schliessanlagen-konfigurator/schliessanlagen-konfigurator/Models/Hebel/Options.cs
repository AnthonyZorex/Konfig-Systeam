using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models.Hebelzylinder
{
    public class Options
    {
        public int Id { get; set; }
        public int? OptioId { get; set; }
        public Hebelzylinder_Options Option { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }

        public ICollection<Options_value> Options_value { get; set; }
        public Options()
        {
            Options_value = new List<Options_value>();
        }
    }
}
