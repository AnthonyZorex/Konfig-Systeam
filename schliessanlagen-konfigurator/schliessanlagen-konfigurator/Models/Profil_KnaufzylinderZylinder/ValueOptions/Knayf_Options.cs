using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder.ValueOptions
{
    public class Knayf_Options
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public Profil_Knaufzylinder_Options Options { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }

        public ICollection<Knayf_Options_value> Knayf_Options_value { get; set; }
        public Knayf_Options()
        {
            Knayf_Options_value = new List<Knayf_Options_value>();
        }
    }
}
