using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models.Aussen_Rund
{
    public class Aussen_Rund_all
    {
        public int Id { get; set; }
        public int? Aussen_Rund_optionsId { get; set; }
        public Aussen_Rund_options Aussen_Rund_options { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }

        public ICollection<Aussen_Rouns_all_value> Aussen_Rouns_all_value { get; set; }
        public Aussen_Rund_all()
        {
            Aussen_Rouns_all_value = new List<Aussen_Rouns_all_value>();
        }
    }
}
