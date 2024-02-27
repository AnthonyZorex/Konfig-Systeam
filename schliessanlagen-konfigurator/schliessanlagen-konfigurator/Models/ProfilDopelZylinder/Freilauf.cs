using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;

namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Freilauf
    {
        public int Id { get; set; }
        public int? dopelOptionsId { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }
        public ICollection<Freilauf_Value> Freilauf_Value { get; set; }
        public Freilauf()
        {
            Freilauf_Value = new List<Freilauf_Value>();
        }

    }
}
