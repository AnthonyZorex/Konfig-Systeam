using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models
{
    public class Profil_Doppelzylinder_Options
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public float Cost { get; set; }
        public string Description { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public string? NGF { get; set; }
        public string? Freilauf { get; set; }
        public string? Zylinderfaerbung { get; set; }
        public string? Bohrschutz { get; set; }
        public string? Witterungsschutz { get; set; }
        public string ? Schliessbart {  get; set; }

      

        //public string? NG_Funktion { get; set; }
        //public bool? Freilauf { get; set; }
        //public string? Zylinderfärbung { get; set; }
        //public string? Bohrschutz { get; set; }
        //public string? Witterungsschutz { get; set; }
        //public string? Schließbart { get; set; }
        //public string? Bügelhöhe { get; set; }
        //public string? Zylinderknauf { get; set; }
        //public string? Schließweg { get; set; }
        //public string? Schließhebel { get; set; }
        //public string? Modulbauweise { get; set; }
        //public string? Zubehör { get; set; }

    }
}
