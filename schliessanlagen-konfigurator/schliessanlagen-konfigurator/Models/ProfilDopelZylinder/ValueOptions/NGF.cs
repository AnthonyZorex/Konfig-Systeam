using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions
{
    public class NGF
    {
        public int Id { get; set; }
        public int? OptionsId { get; set; }
        public Profil_Doppelzylinder_Options Options { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }
        public ICollection<NGF_Value> NGF_Value { get; set; }
        public NGF()
        {
            NGF_Value = new List<NGF_Value>();
        }
    }
}
