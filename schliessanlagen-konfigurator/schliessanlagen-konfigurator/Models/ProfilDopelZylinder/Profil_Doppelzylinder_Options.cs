using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;


namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder
{
    public class Profil_Doppelzylinder_Options
    {
        public int Id { get; set; }
        public int? DoppelzylinderId { get; set; }
        public Profil_Doppelzylinder Doppelzylinder { get; set; }
        public ICollection<NGF> NGF { get; set; }    
        public Profil_Doppelzylinder_Options()
        {
            NGF = new List<NGF>();
        }
    }
}
