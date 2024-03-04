using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions
{
    public class Halbzylinder_Options
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public Profil_Halbzylinder_Options Options { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }

        public ICollection<Halbzylinder_Options_value> Halbzylinder_Options_value { get; set; }
        public Halbzylinder_Options()
        {
            Halbzylinder_Options_value = new List<Halbzylinder_Options_value>();
        }
    }
}
