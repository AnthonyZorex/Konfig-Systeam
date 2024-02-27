using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Profil_Doppelzylinder_Options
    {
        public int Id { get; set; }
        public int? DoppelzylinderId { get; set; }
        public Profil_Doppelzylinder Doppelzylinder { get; set; }
        public ICollection<NGF> NGF { get; set; }
        public ICollection<Freilauf> Freilauf { get; set; }
        public ICollection<Zylinderfaerbung> Zylinderfaerbung { get; set; }
        public ICollection<Bohrschutz> Bohrschutz { get; set; }
        public ICollection<Witterungsschutz> Witterungsschutz { get; set; }
        public ICollection<Schliessbart> Schliessbart { get; set; }
        public Profil_Doppelzylinder_Options()
        {
            NGF = new List<NGF>();
            Freilauf = new List<Freilauf>();
            Zylinderfaerbung = new List<Zylinderfaerbung>();
            Bohrschutz = new List<Bohrschutz>();
            Witterungsschutz = new List<Witterungsschutz>();
            Schliessbart = new List<Schliessbart>();
        }
    }
}
