using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models
{
    public class SystemOptionInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? OptionsId { get; set; }
        public SystemOptionen Options { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }

    }
}
